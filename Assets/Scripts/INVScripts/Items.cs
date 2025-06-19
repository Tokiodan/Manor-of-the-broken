using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Items")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public Enums.ItemType itemType;  // Updated reference to Enums.ItemType
    public GameObject prefab;        // For item prefab

    public string GetFormattedName() => $"{itemType} {itemName}";
}
