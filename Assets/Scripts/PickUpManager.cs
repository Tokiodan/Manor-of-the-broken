using UnityEngine;
using TMPro;

public class PickupManager : MonoBehaviour
{
    [Header("Setup")]
    public string Name;
    public TextMeshProUGUI interactionText;

    [Header("Item Display")]
    public GameObject itemPrefab;           // The prefab to instantiate
    public Transform[] displaySlots;        // 6 possible locations

    private bool isPlayerInRange = false;

    private void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            TryPlaceItemInDisplay();
        }
    }

    private void TryPlaceItemInDisplay()
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("No itemPrefab assigned to PickupManager!");
            return;
        }

        foreach (Transform slot in displaySlots)
        {
            if (slot.childCount == 0)
            {
                Instantiate(itemPrefab, slot.position, slot.rotation, slot);

                if (interactionText != null)
                    interactionText.gameObject.SetActive(false);

                Destroy(gameObject);
                return;
            }
        }

        Debug.Log("All display slots are full.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionText != null)
                interactionText.gameObject.SetActive(true);
                interactionText.text = $"Press [ENTER] to pick up {Name}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionText != null)
                interactionText.gameObject.SetActive(false);
        }
    }
}
