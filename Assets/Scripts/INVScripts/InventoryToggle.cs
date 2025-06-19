using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI; 

    //This code is used in order to make the inventory appear or disappear,
    //Let's say, you press keycode "I", then the inventory UI should appear.
    //However, when the same keycode is pressed back again, then the inventory UI will disappear.

    private bool isInventoryVisible = false;

    void Update()
    {
        //Switch between true or false
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryVisible = !isInventoryVisible;
            inventoryUI.SetActive(isInventoryVisible);
        }
    }
}
