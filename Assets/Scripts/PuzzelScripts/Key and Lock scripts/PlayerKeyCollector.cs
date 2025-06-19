using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerKeyCollector : MonoBehaviour
{
    private List<string> collectedItems = new List<string>();

    private KeyItem nearbyItem;
    private LockItem nearbyReceiver;

    [Header("UI")]
    public TextMeshProUGUI interactionPrompt;

    void Update()
    {
        // === UI Prompt Logic ===
        if (nearbyItem != null)
        {
            interactionPrompt.text = $"Press [Enter] to pick up {nearbyItem.itemName}";
            interactionPrompt.enabled = true;
        }
        else if (nearbyReceiver != null)
        {
            if (collectedItems.Contains(nearbyReceiver.requiredItemID))
            {
                interactionPrompt.text = $"Press [Enter] to use {GetItemNameByID(nearbyReceiver.requiredItemID)} on {nearbyReceiver.receiverName}";
            }
            else
            {
                interactionPrompt.text = $"You need {GetItemNameByID(nearbyReceiver.requiredItemID)} to unlock {nearbyReceiver.receiverName}";
            }

            interactionPrompt.enabled = true;
        }
        else
        {
            interactionPrompt.enabled = false;
        }

        // === Interaction Logic ===
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (nearbyItem != null)
            {
                collectedItems.Add(nearbyItem.itemID);
                Destroy(nearbyItem.gameObject);
                nearbyItem = null;
                interactionPrompt.enabled = false;
            }

            if (nearbyReceiver != null && collectedItems.Contains(nearbyReceiver.requiredItemID))
            {
                Destroy(nearbyReceiver.gameObject);
                nearbyReceiver = null;
                interactionPrompt.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out KeyItem item))
        {
            nearbyItem = item;
        }

        if (other.TryGetComponent(out LockItem receiver))
        {
            nearbyReceiver = receiver;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out KeyItem item) && item == nearbyItem)
        {
            nearbyItem = null;
        }

        if (other.TryGetComponent(out LockItem receiver) && receiver == nearbyReceiver)
        {
            nearbyReceiver = null;
        }
    }

    // Utility: Get item name from your list or fallback to ID
    private string GetItemNameByID(string id)
    {
        // You could optionally expand this to a dictionary or real inventory system.
        // This just returns a capitalized version as fallback.
        if (nearbyItem != null && nearbyItem.itemID == id)
            return nearbyItem.itemName;

        return FormatID(id);
    }

    // Optional fallback formatter (e.g., "golden_idol" => "Golden Idol")
    private string FormatID(string raw)
    {
        string[] parts = raw.Split('_');
        for (int i = 0; i < parts.Length; i++)
            parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
        return string.Join(" ", parts);
    }
}
