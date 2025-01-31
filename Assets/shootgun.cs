using UnityEngine;

public class shootgun : MonoBehaviour
{
    public Transform bulletSpawner;
    public GameObject bulletprefab;
    public float bulletSpeed = 10;

    void Update()
    {
        var bullet = Instantiate(bulletprefab, bulletSpawner.position, bulletSpawner.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawner.forward * bulletSpeed;
    }
}
