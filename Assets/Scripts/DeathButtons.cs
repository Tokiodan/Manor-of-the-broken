using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathButtons : MonoBehaviour
{
    public void OnRespawnClicked()
    {
        GameManager.Instance.RespawnPlayer();
    }
}
