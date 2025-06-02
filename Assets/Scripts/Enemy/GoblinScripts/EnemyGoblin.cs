using UnityEngine;
using Audio;

public class EnemyGoblin : EnemyController
{

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
        AudioManager.Instance.PlaySFX(Audio.SFXType.GoblinEnemyDeath);
        animator.SetTrigger("isDying");
        base.Die();
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySFX(Audio.SFXType.GoblinEnemyAttack);
        base.Attack();
    }

    public override void Move(Vector3 direction)
    {
        base.Move(direction);
    }


}
