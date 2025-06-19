using UnityEngine;

public class RotationPuzzleManager : MonoBehaviour
{
    public RotatableItem[] Items;
    public GameObject doorToOpen;
    public float rotationTolerance = 5f;

    [HideInInspector]
    public bool puzzleSolved = false;

    void Update()
    {
        if (!puzzleSolved && AllItemsCorrect())
        {
            OpenDoor();
            puzzleSolved = true;
            enabled = false; // optional, stop checking after solved
        }
    }

    bool AllItemsCorrect()
    {
        foreach (var item in Items)
        {
            if (!item.IsCorrectRotation(rotationTolerance))
                return false;
        }
        return true;
    }

    void OpenDoor()
    {
        if (doorToOpen != null)
        {
            doorToOpen.SetActive(false);
            Debug.Log("Door opened! Puzzle solved.");
        }
    }
}
