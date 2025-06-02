using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public float attackDamage = 20f;
    private bool canDealDamage = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!canDealDamage) return;

        GameObject target = other.gameObject;

        // 💥 If target is dead/destroyed or disabled — get out
        if (!target.activeInHierarchy) return;

        if (!target.CompareTag("Enemy")) return;

        EnemyHealth enemyHealth = target.GetComponentInChildren<EnemyHealth>();
        if (enemyHealth == null) return;
        if (enemyHealth.isDead) return;

        enemyHealth.TakeDamage(attackDamage);
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