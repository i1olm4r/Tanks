using UnityEngine;

public class TankShooter : MonoBehaviour
{
    [SerializeField] private TankConfig config;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Collider ownerCollider;

    private float nextFireTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryShoot();
    }

    private void TryShoot()
    {
        if (config == null ||
            config.projectilePrefab == null ||
            firePoint == null)
        {
            Debug.LogWarning(
                "TankShooter налаштований не повністю."
            );
            return;
        }

        if (Time.time < nextFireTime)
            return;

        Projectile projectile = Instantiate(
            config.projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        projectile.Initialize(
            config.damage,
            config.projectileSpeed,
            config.projectileLifetime,
            ownerCollider
        );

        nextFireTime = Time.time + config.fireCooldown;
    }
}