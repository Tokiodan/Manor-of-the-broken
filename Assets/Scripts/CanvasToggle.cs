using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public Canvas targetCanvas;         // The canvas to toggle
    public GameObject playerObject;     // Assign your player prefab here
    public Camera uiCamera;             // The dedicated UI camera to enable/disable
    public KeyCode toggleKey = KeyCode.E;

    private bool isMenuOpen = false;

    void Start()
    {
        if (targetCanvas != null)
            targetCanvas.enabled = false;

        if (playerObject == null)
            playerObject = GameObject.FindWithTag("Player"); // fallback

        if (uiCamera != null)
            uiCamera.enabled = false;  // Disable UI camera by default
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
            targetCanvas.enabled = isMenuOpen;

        if (playerObject != null)
            playerObject.SetActive(!isMenuOpen); // disable player GameObject

        if (uiCamera != null)
            uiCamera.enabled = isMenuOpen;  // enable UI camera only when menu is open
    }
}
