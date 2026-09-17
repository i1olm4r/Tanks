using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 30;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        TankHealth health = other.GetComponentInParent<TankHealth>();

        if (health != null && !health.IsDead)
        {
            health.RestoreHealth(healAmount);

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(
                    pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}