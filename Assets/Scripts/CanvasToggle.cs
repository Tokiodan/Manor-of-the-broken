using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public Canvas targetCanvas; // toggles the canvas
    public GameObject playerObject;
    public Camera uiCamera;
    public KeyCode toggleKey = KeyCode.E;

    private bool isMenuOpen = false;

    void Start()
    {
        if (targetCanvas != null)
            targetCanvas.enabled = false;

        if (playerObject == null)
            playerObject = GameObject.FindWithTag("Player"); // fallback if not assigned in inspector

        if (uiCamera != null)
            uiCamera.enabled = false;  
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (targetCanvas != null)
        {
            targetCanvas.enabled = isMenuOpen;

            if (targetCanvas.renderMode == RenderMode.ScreenSpaceCamera)
                targetCanvas.worldCamera = uiCamera;
        }

        if (playerObject != null)
            playerObject.SetActive(!isMenuOpen);

        if (uiCamera != null)
        {
            uiCamera.gameObject.SetActive(isMenuOpen);
            uiCamera.tag = isMenuOpen ? "MainCamera" : "Untagged"; 
        }

        Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuOpen;
    }


}
