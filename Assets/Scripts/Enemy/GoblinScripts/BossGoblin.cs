using UnityEngine;
using Audio;


public class BossGoblin : EnemyController
{
    protected override string AttackTriggerName => "GoblinBossAttack";
    protected override string DeathTriggerName => "GoblinBossDeath";
    public override float MaxHealth => 200f;
    public override float MoveSpeed => 1.5f;
    public override float AttackDamage => 30f;
    private bool wasChasing = false;

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
        base.Die();
        if (isDying) return;
        AudioManager.Instance.PlaySFX(Audio.SFXType.GoblinEnemyDeath);
        animator.SetTrigger(DeathTriggerName);
        base.Die();
        GameManager.Instance.Victory();
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
            wasChasing = false;
            return;
        }

        bool isCurrentlyChasing = direction.sqrMagnitude > 0.01f;

        if (isCurrentlyChasing && !wasChasing)
        {
            AudioManager.Instance.PlayContinuousSFX(SFXType.GoblinBossRun); // Start run sound
            wasChasing = true;
        }
        else if (!isCurrentlyChasing && wasChasing)
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinBossRun); // Stop run sound
            wasChasing = false;
        }

        base.Move(direction);
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinBossAttack);
        base.Attack();
    }

}