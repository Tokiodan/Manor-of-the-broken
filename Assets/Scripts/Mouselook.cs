using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;

    private float rotationX = 0;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<PlayerController>().playerCamera;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
