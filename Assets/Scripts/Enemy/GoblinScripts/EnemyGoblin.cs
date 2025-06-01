using UnityEngine;

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

            // Keep facing player while attacking
            if (playerTransform != null)
            {
                animator.SetFloat("Speed", 0f); // maybe 0 for pure attack
            }
        }
        else
        {
            animator.SetBool("isPlayerThere", false);
            animator.SetFloat("Speed", 0f); // Not attacking/moving
        }
    }

    public void KillGoblin()
    {
        animator.SetTrigger("isDying");
        Die(); // internally call the protected one
    }

}
