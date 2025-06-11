using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private ItemGrid selectedItemGrid;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image draggedItemIcon;

    private ItemData draggedItem;
    private ItemSlot originalSlot;

    private void Update()
    {
        if (selectedItemGrid == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            ItemSlot clickedSlot = selectedItemGrid.GetSlotUnderMouse();
            if (clickedSlot != null && clickedSlot.HasItem())
            {
                draggedItem = clickedSlot.GetItem();
                originalSlot = clickedSlot;

                draggedItemIcon.sprite = draggedItem.icon;
                draggedItemIcon.enabled = true;
                draggedItemIcon.transform.position = Input.mousePosition;

                clickedSlot.ClearItem();
            }
        }

        if (draggedItem != null)
        {
            draggedItemIcon.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                ItemSlot releasedSlot = selectedItemGrid.GetSlotUnderMouse();
                if (releasedSlot != null)
                {
                    if (!releasedSlot.HasItem())
                    {
                        releasedSlot.SetItem(draggedItem);
                    }
                    else
                    {
                        // Swap
                        ItemData temp = releasedSlot.GetItem();
                        releasedSlot.SetItem(draggedItem);
                        originalSlot.SetItem(temp);
                    }
                }
                else
                {
                    originalSlot.SetItem(draggedItem); // Drop failed, return
                }

                draggedItem = null;
                draggedItemIcon.enabled = false;
            }
        }
    }
}
