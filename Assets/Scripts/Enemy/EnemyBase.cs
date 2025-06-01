using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float moveSpeed = 5f;
    public GameObject coinPrefab;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by damage taken
        if (currentHealth <= 0) // Check if health is less than or equal to 0
        {
            Die(); // Call the Die function if health is 0 or less
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
}
