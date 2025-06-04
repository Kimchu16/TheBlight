using UnityEngine;

public class GoblinAttackHitbox : EnemyAttackHitbox
{
    // Call this when goblin is swinging
    public new void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}

