using UnityEngine;
using System.Collections;

public class VaultController : MonoBehaviour
{
    public float vaultDistance = 1.5f;
    public float vaultHeight = 1f;
    public float vaultDuration = 0.5f;
    public bool IsVaulting { get; private set; }

    private PlayerController player;

    void Start() { player = GetComponent<PlayerController>(); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsVaulting && player.staminaSystem.CurrentStamina >= player.staminaSystem.vaultStaminaCost)
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out RaycastHit hit, vaultDistance))
            {
                if (hit.transform.CompareTag("Vaultable"))
                    StartCoroutine(Vault(hit));
            }
        }
    }

    IEnumerator Vault(RaycastHit hit)
    {
        IsVaulting = true;
        player.staminaSystem.UseStamina(player.staminaSystem.vaultStaminaCost);

        Vector3 start = transform.position;
        Vector3 end = hit.point + Vector3.up * vaultHeight + transform.forward * 0.5f;
        float elapsed = 0;

        while (elapsed < vaultDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / vaultDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        IsVaulting = false;
    }
}
