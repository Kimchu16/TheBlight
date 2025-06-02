using UnityEngine;
using Audio;

public class EliteGoblin : EnemyController
{
    protected override string AttackTriggerName => "GoblinEliteAttack";
    protected override string DeathTriggerName => "GoblinEliteDeath";

    protected override void Update()
    {
        base.Update();

        if (playerInRange)
        {
            animator.SetBool("isPlayerThere", true);

            if (Time.time >= lastAttackTime + attackCooldown) // 20% faster
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            animator.SetBool("isPlayerThere", false);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage * 0.8f); // 20% damage reduction
    }

    public override void Die()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinEliteDeath);
        animator.SetTrigger(DeathTriggerName);
        base.Die();
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinEliteAttack);
        base.Attack();
    }

        public override void Move(Vector3 direction)
    {
        //AudioManager.Instance.PlaySFX(SFXType.GoblinBossRun);
        base.Move(direction);
    }
}


