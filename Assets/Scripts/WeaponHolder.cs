using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject CurrentWeapon { get; private set; }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon);
        }

        CurrentWeapon = Instantiate(weaponPrefab, weaponSlot.position, weaponSlot.rotation, weaponSlot);
    }
}

