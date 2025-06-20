using UnityEngine;

public class GameManager : MonoBehaviour
{
    // manages player respawning and checkpoint saving/loading
    public static GameManager Instance;
    public GameObject playerPrefab;
    public Transform respawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCheckpoint(); // get the last saved one
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        // set a new checkpoint and save its position
        respawnPoint = checkpoint;

        PlayerPrefs.SetFloat("RespawnX", checkpoint.position.x);
        PlayerPrefs.SetFloat("RespawnY", checkpoint.position.y);
        PlayerPrefs.SetFloat("RespawnZ", checkpoint.position.z);
        PlayerPrefs.Save();

        Debug.Log($"Checkpoint saved at {checkpoint.position}");
    }

    public void LoadCheckpoint()
    {
        // load saved checkpoint position from playerprefs
        if (PlayerPrefs.HasKey("RespawnX"))
        {
            Vector3 savedPos = new Vector3(
                PlayerPrefs.GetFloat("RespawnX"),
                PlayerPrefs.GetFloat("RespawnY"),
                PlayerPrefs.GetFloat("RespawnZ")
            );

            // create a temporary transform at the saved position
            GameObject temp = new GameObject("LoadedCheckpoint");
            temp.transform.position = savedPos;
            respawnPoint = temp.transform;

            Debug.Log($"Loaded checkpoint at {savedPos}");
        }
    }

    public void RespawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not set!");
            return;
        }

        Vector3 spawnPos = respawnPoint ? respawnPoint.position : Vector3.zero;
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        // restore player data like position and points
        player.GetComponent<PlayerSaveManager>()?.Load();
    }
}
