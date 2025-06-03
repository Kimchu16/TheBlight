using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public float attackDamage = 10f;
    private bool canDealDamage = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!canDealDamage) return;

        GameObject target = other.gameObject;

        if (!target.activeInHierarchy) return;

        if (!target.CompareTag("Enemy")) return;

        EnemyHealth enemyHealth = target.GetComponentInChildren<EnemyHealth>();
        if (enemyHealth == null) return;
        if (enemyHealth.isDead) return;

        enemyHealth.TakeDamage(attackDamage);

        canDealDamage = false;
        gameObject.SetActive(false);
    }

    public void EnableAttack()
    {
        canDealDamage = true;
        gameObject.SetActive(true);
    }

    public void DisableAttack()
    {
        canDealDamage = false;
        gameObject.SetActive(false);
    }
}