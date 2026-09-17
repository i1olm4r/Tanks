using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankHealthBar : MonoBehaviour
{
    [SerializeField] private TankHealth health;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text healthText;

    private void OnEnable()
    {
        if (health == null)
            return;

        health.HealthChanged += UpdateView;
        UpdateView(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDisable()
    {
        if (health != null)
            health.HealthChanged -= UpdateView;
    }

    private void UpdateView(int currentHealth, int maxHealth)
    {
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = maxHealth;
            slider.value = currentHealth;

            float ratio = (float)currentHealth / maxHealth;
            Image fill = slider.fillRect.GetComponent<Image>();

            if (fill != null)
            {
                if (ratio > 0.6f) fill.color = Color.green;
                else if (ratio > 0.3f) fill.color = Color.yellow;
                else fill.color = Color.red;
            }
        }

        if (healthText != null)
        {
            healthText.text =
                currentHealth + " / " + maxHealth;
        }
    }
}