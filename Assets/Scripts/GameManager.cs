using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;         // Assign in Inspector
    public Transform respawnPoint;          // Assign in Inspector

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When entering a gameplay scene, spawn the player
        if (scene.name == "Respawn") // or any other gameplay scene
        {
            if (playerPrefab != null && respawnPoint != null)
            {
                Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
            }
        }

        // When entering the death scene, unlock the cursor
        if (scene.name == "Death")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void KillPlayer()
    {
        // Called when the player touches the death trigger
        SceneManager.LoadScene("Death");
    }

    public void Respawn()
    {
        // Called by UI button
        SceneManager.LoadScene("Respawn");
    }

    public void RespawnPlayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Respawn");
    }

}
