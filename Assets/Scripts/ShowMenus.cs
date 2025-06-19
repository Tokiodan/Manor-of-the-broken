using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenus : MonoBehaviour
{
    public GameObject Inventory;
    public bool IsMenuOpen;

    void Start()
    {
        IsMenuOpen = false;
        Inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!IsMenuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Inventory.SetActive(true);
                Time.timeScale = 0f;
                IsMenuOpen = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Inventory.SetActive(false);
                Time.timeScale = 1f;
                IsMenuOpen = false;
            }
        }
    }
}
