using UnityEngine;

public class InventoryLoader : MonoBehaviour
{
    public ItemSlot[] itemSlots; // Drag all 10 slots here
    public ItemData[] startingItems; // Add Potion here

    void Start()
    {
        for (int i = 0; i < startingItems.Length && i < itemSlots.Length; i++)
        {
            itemSlots[i].SetItem(startingItems[i]);
        }
    }
}

