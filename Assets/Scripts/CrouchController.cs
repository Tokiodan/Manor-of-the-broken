using UnityEngine;

public class CrouchController : MonoBehaviour
{
    public float crouchScale = 0.5f;
    public float standingScale = 1f;
    public float crouchSpeed = 10f; // Crouchspeed in deze script = Hoe snel je transition is van staand --> Crouchend
    public float crouchOffset = -0.5f;

    public bool IsCrouching { get; private set; }
    private Vector3 standCamPos;
    private Vector3 crouchCamPos;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<PlayerController>().playerCamera;
        standCamPos = cam.transform.localPosition;
        crouchCamPos = standCamPos + new Vector3(0, crouchOffset, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouching = !IsCrouching;
            Vector3 targetScale = IsCrouching ? new Vector3(1, crouchScale, 1) : new Vector3(1, standingScale, 1);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, crouchSpeed * Time.deltaTime);
            Vector3 camTarget = IsCrouching ? crouchCamPos : standCamPos;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, camTarget, crouchSpeed * Time.deltaTime);
        }
    }
}