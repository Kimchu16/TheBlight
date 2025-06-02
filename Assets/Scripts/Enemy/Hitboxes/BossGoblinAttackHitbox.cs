using UnityEngine;

public class BossGoblinAttackHitbox : EnemyAttackHitbox
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerCharacter>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null && other.gameObject == player.gameObject)
            {
                player = null;
            }
        }
    }

    // Call this when boss swings
    public new void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount * 1.5f); // Boss does 50% more damage
        }
    }
}

