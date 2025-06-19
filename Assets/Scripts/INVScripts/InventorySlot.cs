using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public Image icon;
    public Tooltip tooltip;

    private bool isHovering = false;
    private readonly Vector3 baseScale = Vector3.one * 0.75f;
    private readonly Vector3 hoverScale = Vector3.one * 0.825f;
    public Enums.ItemType itemType;  // For the type of the item
    


    private void Start()
    {
        if (icon != null)
            icon.transform.localScale = baseScale;
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            Transform iconTransform = transform.GetChild(0);
            DraggableItem draggable = iconTransform.GetComponent<DraggableItem>();

            if (draggable != null && draggable.transform.parent == transform)
            {
                Vector3 targetScale = isHovering ? hoverScale : baseScale;
                iconTransform.localScale = Vector3.Lerp(iconTransform.localScale, targetScale, Time.deltaTime * 10);
            }
        }
    }

    public void SetItem(Items item, Sprite iconSprite)
    {
        if (transform.childCount > 0)
            return; // Slot already has an item

        GameObject iconObject = new GameObject("Icon");
        iconObject.transform.SetParent(transform);
        iconObject.transform.localPosition = Vector3.zero;
        iconObject.transform.localScale = baseScale;

        Image newIcon = iconObject.AddComponent<Image>();
        newIcon.sprite = iconSprite;
        newIcon.raycastTarget = true;

        CanvasGroup cg = iconObject.AddComponent<CanvasGroup>();
        DraggableItem draggable = iconObject.AddComponent<DraggableItem>();

        draggable.itemData = item;
        draggable.tooltip = tooltip;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 1)
            return; // Slot already filled

        DraggableItem draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggable != null)
        {
            draggable.originalParent = transform;
            draggable.transform.SetParent(transform);
            draggable.transform.localPosition = Vector3.zero;
            draggable.transform.localScale = baseScale;
        }
    }
}
