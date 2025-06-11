using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;

    [SerializeField] public ItemSlot[] itemSlots;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        Vector2 positionOnTheGrid = new Vector2
        {
            x = mousePosition.x - rectTransform.position.x,
            y = rectTransform.position.y - mousePosition.y
        };

        Vector2Int tileGridPosition = new Vector2Int
        {
            x = (int)(positionOnTheGrid.x / tileSizeWidth),
            y = (int)(positionOnTheGrid.y / tileSizeHeight)
        };

        return tileGridPosition;
    }

    public ItemSlot GetSlotUnderMouse()
    {
        foreach (var slot in itemSlots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                slot.GetComponent<RectTransform>(), Input.mousePosition))
            {
                return slot;
            }
        }
        return null;
    }
}
