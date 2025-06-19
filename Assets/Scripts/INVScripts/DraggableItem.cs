using UnityEngine.EventSystems;
using UnityEngine;


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [HideInInspector] public Transform originalParent;
    public Enums.ItemType itemType;  // For the type of the item
    


    public Items itemData; // Reference to Item (updated from ItemData)
    public Tooltip tooltip;

    private void Awake()
    {
        transform.localScale = Vector3.one * 0.75f;

        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;

        originalScale = transform.localScale;
        transform.localScale = Vector3.one * 0.825f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.one * 0.75f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null && itemData != null)
            tooltip.ShowTooltip(itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip?.HideTooltip();
    }
}
