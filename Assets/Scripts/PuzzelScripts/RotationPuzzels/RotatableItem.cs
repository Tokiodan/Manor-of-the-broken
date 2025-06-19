using UnityEngine;

public class RotatableItem : MonoBehaviour
{
    public float targetYRotation;         // Target Y rotation in degrees
    public float rotationStep = 90f;      // How much to rotate per interaction

    public float currentYRotation => transform.eulerAngles.y;

    // Rotate only around Y axis
    public void Rotate()
    {
        Vector3 currentEuler = transform.eulerAngles;
        currentEuler.y = (currentEuler.y + rotationStep) % 360f;
        transform.eulerAngles = currentEuler;
    }

    // Check if current rotation is close enough to target
    public bool IsCorrectRotation(float tolerance = 5f)
    {
        float delta = Mathf.Abs(Mathf.DeltaAngle(currentYRotation, targetYRotation));
        return delta <= tolerance;
    }
}
