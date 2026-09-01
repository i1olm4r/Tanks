using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRigidbody;
    private int damage;
    private float lifetime;
    private Collider ownerCollider;
    private bool isInitialized;

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(
        int newDamage,
        float speed,
        float newLifetime,
        Collider newOwnerCollider
    )
    {
        damage = newDamage;
        lifetime = newLifetime;
        ownerCollider = newOwnerCollider;
        isInitialized = true;

        if (ownerCollider != null &&
            TryGetComponent(out Collider projectileCollider))
        {
            Physics.IgnoreCollision(
                projectileCollider,
                ownerCollider
            );
        }

        projectileRigidbody.linearVelocity =
            transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isInitialized)
            return;

        if (collision.collider == ownerCollider)
            return;

        if (collision.collider.TryGetComponent(
            out DamageableTarget target))
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}