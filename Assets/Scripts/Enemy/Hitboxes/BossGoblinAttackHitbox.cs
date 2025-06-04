using Unity.VisualScripting;
using UnityEngine;

public class BossGoblinAttackHitbox : EnemyAttackHitbox
{

    // Call this when boss swings
    public override void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount * 1.8f); // Boss does 50% more damage
        }
    }
}

