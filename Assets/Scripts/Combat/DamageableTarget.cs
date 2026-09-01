using UnityEngine;

public class DamageableTarget : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(
            $"{name} отримав {damage} шкоди. " +
            $"Залишилося HP: {currentHealth}"
        );

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}