using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;

    public void Setup(Items item)
    {
        if (nameText != null)
            nameText.text = item.itemName;

        if (icon != null && item.icon != null)
            icon.sprite = item.icon; // Assign the icon to the image component
    }
}
