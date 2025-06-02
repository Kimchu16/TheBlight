using UnityEngine;

public class EliteGoblinAttackHitbox : EnemyAttackHitbox
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

    // Call this when elite goblin swings
    public new void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount * 1.2f); // Elite does 20% more damage
        }
    }
}
