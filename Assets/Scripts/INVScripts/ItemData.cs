using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
   
}

public enum ItemType
{
    Item,
    HealingMedicine,
    Weapon,
    Ammo,
    Flashlight,
    Battery
}
