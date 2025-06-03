using UnityEngine;
using Audio;

public class EnemyGoblin : EnemyController
{
    public override float MaxHealth => 40f;
    public override float MoveSpeed => 2.5f;
    public override float AttackDamage => 7f;
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
            animator.SetBool("isPlayerThere", false); // Not attacking/moving
        }
    }

    public override void Die()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        base.Die();
        AudioManager.Instance.PlaySFX(Audio.SFXType.GoblinEnemyDeath);
        animator.SetTrigger("isDying");
        
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(Audio.SFXType.GoblinEnemyAttack);
        base.Attack();
    }

    public override void Move(Vector3 direction)
    {
        if (isDying)
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinEnemyRun);
            wasChasing = false;
            return;
        }

        bool isCurrentlyChasing = direction.sqrMagnitude > 0.01f;

        if (isCurrentlyChasing && !wasChasing)
        {
            AudioManager.Instance.PlayContinuousSFX(SFXType.GoblinEnemyRun); // Start run sound
            wasChasing = true;
        }
        else if (!isCurrentlyChasing && wasChasing)
        {
            AudioManager.Instance.StopContinuousSFX(SFXType.GoblinEnemyRun); // Stop run sound
            wasChasing = false;
        }

        base.Move(direction);
    }


}
