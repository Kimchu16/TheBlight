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
        if (isDying) return;
        AudioManager.Instance.PlaySFX(Audio.SFXType.GoblinEnemyDeath);
        animator.SetTrigger(DeathTriggerName);
        base.Die();
    }

    protected override void OnDeathComplete()
    {
        GameManager.Instance.Victory();
    }

    public override void Move(Vector3 direction)
    {
        if (isDying)
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinBossRun);
            return;
        }
        if (isChasing)
        {
            AudioManager.Instance.PlayContinuousSFX(SFXType.GoblinBossRun);
        }
        else
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinBossRun);
        }
        base.Move(direction);
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinBossAttack);
        base.Attack();
    }

}