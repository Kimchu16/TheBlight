using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthFillImage;
    public float maxHealth = 20f;
    private float currentHealth;

    private EnemyGoblin enemyGoblin;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        enemyGoblin = GetComponent<EnemyGoblin>();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemyGoblin.KillGoblin();
        }
        UpdateHealthBar();        // Move this below so health bar updates even at 0
    }


    private void UpdateHealthBar()
    {
        healthFillImage.fillAmount = currentHealth / maxHealth;
    }

}
