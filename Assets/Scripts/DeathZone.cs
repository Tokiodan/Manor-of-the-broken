using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public CheckpointManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.PlayerDied();
        }
    }
}
