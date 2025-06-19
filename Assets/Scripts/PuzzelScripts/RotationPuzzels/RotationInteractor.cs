using UnityEngine;

public class RotationInteractor : MonoBehaviour
{
    private RotatableItem item;
    private bool playerInRange = false;

    public RotationPuzzleManager puzzleManager;

    void Start()
    {
        item = GetComponent<RotatableItem>();

        if (puzzleManager == null)
        {
            puzzleManager = FindObjectOfType<RotationPuzzleManager>();
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            if (item != null && puzzleManager != null && !puzzleManager.puzzleSolved)
            {
                item.Rotate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
