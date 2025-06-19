using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointUI; // assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(transform);
            other.GetComponent<PlayerSaveManager>()?.Save();

            // Show "Checkpoint Reached!" UI
            if (checkpointUI != null)
            {
                checkpointUI.SetActive(true);
                Invoke(nameof(HideUI), 2f); // hide after 2 seconds
            }
        }
    }

    void HideUI()
    {
        if (checkpointUI != null)
            checkpointUI.SetActive(false);
    }
}
