using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUseHandler : MonoBehaviour, IPointerClickHandler
{
    public Items itemData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemData == null) return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        Debug.Log($"Using item: {itemData.itemName} with type: {itemData.itemType}");

        switch (itemData.itemType)
        {
            case Enums.ItemType.HealingMedicine:
                PlayerHP player = FindObjectOfType<PlayerHP>();
                if (player != null)
                {
                    player.Regenerate(25f);  // You can replace 25f with a value from itemData if added
                    Debug.Log("Used HealingMedicine: +25 HP");
                }
                break;

            case Enums.ItemType.Ammo:
                Gun gun = FindObjectOfType<Gun>();
                if (gun != null)
                {
                    gun.AddReserveAmmo(10);
                    Debug.Log("Used Ammo: +10 Reserve");
                }
                break;

            case Enums.ItemType.Battery:
                Flashlight flashlight = FindObjectOfType<Flashlight>();
                if (flashlight != null)
                {
                    flashlight.batteryLife = Mathf.Min(100f, flashlight.batteryLife + 25f);
                    Debug.Log("Used Battery: +25 Battery Life");
                }
                break;

            case Enums.ItemType.Flashlight:
                Debug.Log("Flashlight item used — add specific logic if needed.");
                break;

            case Enums.ItemType.Weapon:
                Debug.Log("Weapon item used — add specific logic if needed.");
                break;

            case Enums.ItemType.Item:
                Debug.Log("Generic item used — add specific logic if needed.");
                break;

            default:
                Debug.LogWarning($"Used unknown item type: {itemData.itemType}");
                break;
        }

        Destroy(gameObject); // Remove the item from UI/inventory after use
    }
}
