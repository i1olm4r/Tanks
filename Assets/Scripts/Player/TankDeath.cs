using UnityEngine;

[RequireComponent(typeof(TankHealth))]
public class TankDeath : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField, Min(0f)] private float destroyDelay = 0.1f;
    [SerializeField] private bool isPlayer = false;

    private TankHealth health;
    private KillCounter killCounter;

    private void Awake()
    {
        health = GetComponent<TankHealth>();
        killCounter = FindFirstObjectByType<KillCounter>();
    }

    private void OnEnable()
    {
        health.Died += HandleDeath;
    }

    private void OnDisable()
    {
        health.Died -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        DisableTank();

        if (killCounter != null && !isPlayer)
            killCounter.RegisterKill();

        if (isPlayer)
        {
            GameOverPanel gameOver = FindFirstObjectByType<GameOverPanel>();
            if (gameOver != null) gameOver.Show();
        }

        Destroy(gameObject, destroyDelay);
    }

    private void DisableTank()
    {
        Collider[] colliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider tankCollider in colliders)
            tankCollider.enabled = false;

        TankShooter shooter = GetComponent<TankShooter>();

        if (shooter != null)
            shooter.enabled = false;
    }
}