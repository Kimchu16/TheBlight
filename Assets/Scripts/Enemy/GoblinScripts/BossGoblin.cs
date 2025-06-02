using UnityEngine;
using Audio;


public class BossGoblin : EnemyController
{
    protected override string AttackTriggerName => "GoblinBossAttack";
    protected override string DeathTriggerName => "GoblinBossDeath";

    protected override void Update()
    {
        base.Update();

        if (playerInRange)
        {
            animator.SetBool("isPlayerThere", true);

            if (Time.time >= lastAttackTime + attackCooldown)
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
        base.TakeDamage(damage * 0.7f); // 30% damage reduction (Boss is tanky)
    }

    public override void Die()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinBossDeath);
        animator.SetTrigger(DeathTriggerName);
        base.Die();
    }

    public override void Move(Vector3 direction)
    {
        //AudioManager.Instance.PlaySFX(SFXType.GoblinBossRun);
        base.Move(direction);
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinBossAttack);
        base.Attack();
    }

}