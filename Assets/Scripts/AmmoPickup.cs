using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 10;
    [SerializeField] private bool inRangeOfAmmoPickup = false;
    public TextMeshProUGUI pickuptext;

    private GameObject player;
    private Gun playerGun;

    void Update()
    {
        if (inRangeOfAmmoPickup && Input.GetKeyDown(KeyCode.F))
        {
            if (playerGun != null)
            {
                playerGun.AddReserveAmmo(ammoAmount);
                Debug.Log("Picked up ammo!");
                pickuptext.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfAmmoPickup = true;
            player = other.gameObject;

            // Try to find Gun script in player's weapon holder
            WeaponHolder holder = player.GetComponent<WeaponHolder>();
            if (holder != null && holder.CurrentWeapon != null)
            {
                playerGun = holder.CurrentWeapon.GetComponent<Gun>();
            }

            pickuptext.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfAmmoPickup = false;
            pickuptext.gameObject.SetActive(false);
            playerGun = null;
        }
    }
}
