using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public Camera cam;

    [Header("Ammo Settings")]
    public int magSize = 10;
    public int maxAmmo = 30;
    public float reloadTime = 2f;

    private int currentAmmo;
    private int currentReserve;
    private bool isReloading = false;

    [Header("Fire Rate")]
    public float fireRate = 0.3f;
    private bool canShoot = true;

    void Start()
    {
        // Auto-assign main camera if none set (useful for prefabs)
        if (cam == null)
            cam = Camera.main;

        currentAmmo = magSize;
        currentReserve = maxAmmo;
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            if (currentAmmo > 0)
            {
                StartCoroutine(ShootCooldown());
            }
            else if (currentReserve > 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
                Debug.Log("No ammo left!");
            }
        }
    }

    public void AddReserveAmmo(int amount)
    {
        currentReserve += amount;
    }

    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }

    public void InitializeAmmo(int startingReserve)
    {
        currentAmmo = magSize;
        currentReserve = startingReserve;
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void Shoot()
    {
        currentAmmo--;
        Debug.Log($"Bang! Ammo in mag: {currentAmmo} | Reserve: {currentReserve}");

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * range, Color.gray, 1f);
        }

        if (currentAmmo <= 0 && currentReserve > 0)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        canShoot = false;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = magSize - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, currentReserve);

        currentAmmo += ammoToLoad;
        currentReserve -= ammoToLoad;

        isReloading = false;
        canShoot = true;
        Debug.Log($"Reloaded! Ammo in mag: {currentAmmo} | Reserve: {currentReserve}");
    }
}
