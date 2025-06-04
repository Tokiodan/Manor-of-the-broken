using UnityEngine;

public class PlayerRespawnOnTrigger : RespawnHandler
{
    public string triggerTag = "DeadZone";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.Respawn();
            }
        }
    }
}
