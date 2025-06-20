using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(transform);
            other.GetComponent<PlayerSaveManager>()?.Save();

            // shows text
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
