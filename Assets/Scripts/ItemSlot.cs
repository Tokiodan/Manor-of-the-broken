using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image iconImage;
    private ItemData currentItem;

    public void SetItem(ItemData item)
    {
        currentItem = item;
        iconImage.enabled = item != null;
        iconImage.sprite = item != null ? item.icon : null;
    }

    public ItemData GetItem()
    {
        return currentItem;
    }

    public void ClearItem()
    {
        currentItem = null;
        iconImage.enabled = false;
        iconImage.sprite = null;
    }

    public bool HasItem()
    {
        return currentItem != null;
    }
}
