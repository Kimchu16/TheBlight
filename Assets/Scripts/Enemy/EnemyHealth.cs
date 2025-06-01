using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthFillImage;
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthFillImage.fillAmount = currentHealth / maxHealth;
    }

    // For testing purpose, reduce health every second
    private void Update()
    {
        if (Time.frameCount % 60 == 0)  // Roughly every second
        {
            TakeDamage(10f);
        }
    }
}
