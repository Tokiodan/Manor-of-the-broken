using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Items> itemsInInventory; // Changed from Item to Items

    public Transform inventoryPanel; // Parent with GridLayoutGroup

    private void Start()
    {
        PopulateInventory();
    }

    private void ClearInventory()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }
    }

    private void PopulateInventory()
    {
        foreach (var item in itemsInInventory)
        {
            if (item.icon != null)  // Use icon from the Items class
            {
                GameObject obj = Instantiate(item.prefab, inventoryPanel); // Ensure prefab is set in Items class
                InventoryItemUI ui = obj.GetComponent<InventoryItemUI>();
                if (ui != null)
                {
                    ui.Setup(item);  // Set up item in the UI
                }
            }
        }
    }
}
