using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        respawnPoint = checkpoint;

        PlayerPrefs.SetFloat("RespawnX", checkpoint.position.x);
        PlayerPrefs.SetFloat("RespawnY", checkpoint.position.y);
        PlayerPrefs.SetFloat("RespawnZ", checkpoint.position.z);
        PlayerPrefs.Save();

        Debug.Log($"Checkpoint saved at {checkpoint.position}");
    }

    public void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("RespawnX"))
        {
            Vector3 savedPos = new Vector3(
                PlayerPrefs.GetFloat("RespawnX"),
                PlayerPrefs.GetFloat("RespawnY"),
                PlayerPrefs.GetFloat("RespawnZ")
            );

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

        player.GetComponent<PlayerSaveManager>()?.Load();
    }
}
