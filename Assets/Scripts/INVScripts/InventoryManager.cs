using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Data")]
    public List<Items> itemsInInventory = new List<Items>();

    [Header("UI")]
    public Transform inventoryPanel; // Parent with GridLayoutGroup

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        RefreshInventoryDisplay();
    }

    public void RefreshInventoryDisplay()
    {
        ClearInventory();
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
        foreach (Items item in itemsInInventory)
        {
            if (item.prefab != null)
            {
                GameObject uiElement = Instantiate(item.prefab, inventoryPanel);

                InventoryItemUI itemUI = uiElement.GetComponent<InventoryItemUI>();
                if (itemUI != null)
                {
                    itemUI.Setup(item);  // Setup icon, name, tooltip, etc.
                }
                else
                {
                    Debug.LogWarning($"Missing InventoryItemUI on {item.prefab.name}");
                }
            }
            else
            {
                Debug.LogWarning($"No prefab assigned for item: {item.itemName}");
            }
        }
    }

    /// <summary>
    /// Attempts to add an item to inventory.
    /// Returns true if success, false if inventory full.
    /// Max inventory size = 6
    /// </summary>
    public bool TryAddItem(Items item)
    {
        if (itemsInInventory.Count >= 6)
        {
            Debug.Log("Inventory full.");
            return false;
        }

        itemsInInventory.Add(item);
        RefreshInventoryDisplay();
        return true;
    }
}
