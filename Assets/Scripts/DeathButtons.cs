using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathButtons : MonoBehaviour
{
    public void OnRespawnClicked()
    {
        GameManager.Instance.RespawnPlayer();
        SceneManager.LoadScene("Respawn"); 
    }

    public void OnExitToMenuClicked()
    {
        PlayerPrefs.Save(); // makes sure everything is saved
        SceneManager.LoadScene("MainMenu"); 
    }
}
