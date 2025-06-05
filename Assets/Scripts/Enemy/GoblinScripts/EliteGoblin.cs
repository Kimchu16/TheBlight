using UnityEngine;
using Audio;

public class EliteGoblin : EnemyController
{
    protected override string AttackTriggerName => "GoblinEliteAttack";
    protected override string DeathTriggerName => "GoblinEliteDeath";
    public override float MaxHealth => 30f;
    public override float MoveSpeed => 5f;
    public override float AttackDamage => 6f;

    private bool wasChasing = false;

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
        base.TakeDamage(damage * 0.95f); // 5% damage reduction
    }

    public override void Die()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        base.Die();
        AudioManager.Instance.PlaySFX(SFXType.GoblinEliteDeath);
        animator.SetTrigger(DeathTriggerName);
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(SFXType.GoblinEliteAttack);
        base.Attack();
    }

    public override void Move(Vector3 direction)
    {
        if (isDying)
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinEliteRun);
            wasChasing = false;
            return;
        }

        bool isCurrentlyChasing = direction.sqrMagnitude > 0.01f;

        if (isCurrentlyChasing && !wasChasing)
        {
            AudioManager.Instance.PlayContinuousSFX(SFXType.GoblinEliteRun); // Start run sound
            wasChasing = true;
        }
        else if (!isCurrentlyChasing && wasChasing)
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinEliteRun); // Stop run sound
            wasChasing = false;
        }

        base.Move(direction);
    }
    
}


