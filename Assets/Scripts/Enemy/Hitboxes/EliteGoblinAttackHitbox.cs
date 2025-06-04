using UnityEngine;

public class EliteGoblinAttackHitbox : EnemyAttackHitbox
{

    // Call this when elite goblin swings
    public override void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount * 1.2f); // Elite does 20% more damage
        }
    }
}
