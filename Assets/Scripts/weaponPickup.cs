using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // The weapon to give the player
    private bool inRange = false;
    private GameObject player;

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            WeaponHolder holder = player.GetComponent<WeaponHolder>();
            if (holder != null)
            {
                holder.EquipWeapon(weaponPrefab);
                Destroy(gameObject); // Destroy pickup after getting the weapon
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            player = null;
        }
    }
}
