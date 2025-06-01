using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public float attackDamage = 20f; // Player attack damage
    private bool canDealDamage = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (canDealDamage && other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponentInChildren<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    public void EnableAttack()
    {
        canDealDamage = true;
    }

    public void DisableAttack()
    {
        canDealDamage = false;
    }
}

