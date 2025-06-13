using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Destroys the player prefab
            GameManager.Instance.KillPlayer();
        }
    }
}
