using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthFillImage;
    private float currentHealth;

    private EnemyBase enemyBase;
    public bool isDead = false;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();

        if (enemyBase == null)
        {
            Debug.LogError("EnemyBase component not found!");
            return;
        }

        currentHealth = enemyBase.MaxHealth;  // Use EnemyBase MaxHealth
        UpdateHealthBar();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;  // Make sure it doesn't go negative
            enemyBase.Die();    // Kill the enemy
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthFillImage != null && enemyBase != null)
        {
            healthFillImage.fillAmount = currentHealth / enemyBase.MaxHealth;  // Use EnemyBase MaxHealth
        }
    }
}
