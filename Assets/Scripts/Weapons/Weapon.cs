using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float bulletLifetime = 3f;

    [Header("Burst Settings")]
    [SerializeField] private int bulletsPerBurst = 3; // Number of bullets per burst
    [SerializeField] private float burstDelay = 1f; // Delay between bursts
    [SerializeField] private float shootingDelay = 0.2f; // Delay between bullets in a burst

    [Header("Ammo Settings")]
    [SerializeField] private int maxBulletCapacity = 50; // Maximum bullets the weapon can hold
    [SerializeField] private float reloadTime = 1f; // Time to reload

    private int currentBulletCount;
    private bool isReloading = false;

    private void Start()
    {
        currentBulletCount = maxBulletCapacity; // Initialize bullet count
        StartCoroutine(AutoBurstFire()); // Start automatic burst firing
    }

    private IEnumerator AutoBurstFire()
    {
        while (true) // Infinite loop for continuous firing
        {
            if (!isReloading && currentBulletCount > 0) // Check if not reloading and has bullets
            {
                for (int i = 0; i < bulletsPerBurst && currentBulletCount > 0; i++) // Fire bullets in burst
                {
                    FireBullet();
                    yield return new WaitForSeconds(shootingDelay); // Delay between bullets in burst
                }
                yield return new WaitForSeconds(burstDelay); // Delay between bursts
            }
            else if (currentBulletCount <= 0 && !isReloading) // Reload if out of bullets
            {
                StartCoroutine(Reload());
            }
            yield return null; // Wait for the next frame
        }
    }

    private void FireBullet()
    {
        if (currentBulletCount <= 0) return; // Don't fire if out of bullets

        currentBulletCount--; // Reduce bullet count
        Vector3 shootingDirection = bulletSpawnPoint.forward; // Shoot in the direction of the spawn point

        // Instantiate and fire the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterDelay(bullet, bulletLifetime)); // Destroy bullet after lifetime
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime); // Wait for reload time
        currentBulletCount = maxBulletCapacity; // Refill bullets
        isReloading = false;
    }

    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet); // Destroy the bullet after delay
    }
}