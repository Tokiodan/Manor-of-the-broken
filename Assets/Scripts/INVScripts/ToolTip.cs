using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Enums;

public class Tooltip : MonoBehaviour
{
    public void ShowTooltip(Items item)
    {
        if (item == null) return;

        Color color = typeColors.ContainsKey(item.itemType) ? typeColors[item.itemType] : Color.white;
        string colorHex = ColorUtility.ToHtmlStringRGB(color);

        titleText.text = $"<color=#{colorHex}>{item.itemName}</color>";
        descriptionText.text = item.description;
        tooltipPanel.SetActive(true);
    }

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject tooltipPanel;

    // New color mapping per ItemType
    private readonly Dictionary<Enums.ItemType, Color> typeColors = new()
{
    { Enums.ItemType.Item, Color.green }, 
    { Enums.ItemType.HealingMedicine, new Color(0.6f, 0.8f, 0.6f) }, //Light greenish for the HERB Medicine
    { Enums.ItemType.Weapon, Color.gray },
    { Enums.ItemType.Ammo, Color.red },
    { Enums.ItemType.Flashlight, Color.cyan },
    { Enums.ItemType.Battery, Color.yellow }

};


    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}


