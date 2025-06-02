using UnityEngine;

public class BossGoblin : EnemyBase
{
    protected override string AttackTriggerName => "GoblinBossAttack"; 
    protected override string DeathTriggerName => "GoblinBossDeath";
    protected void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                return;
        }

        Vector3 targetPosition = player.position;
        Vector2 toPlayer = targetPosition - transform.position;

        float distance = toPlayer.magnitude;

        if (distance > 0.5f)
        {
            Vector2 direction = toPlayer.normalized;
            Move(new Vector3(direction.x, direction.y, 0));

            if (direction.x != 0)
            {
                transform.localScale = new Vector3(originalScale.x * Mathf.Sign(direction.x), originalScale.y, originalScale.z);
            }
        }
        else
        {
            Move(Vector3.zero);
        }

        if (playerInRange)
        {
            animator.SetBool("isPlayerThere", true);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }

            if (playerTransform != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
        else
        {
            animator.SetBool("isPlayerThere", false);
            animator.SetFloat("Speed", 0f);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage * 0.7f); // 30% damage reduction (Boss is tanky)
    }

    public void KillBoss()
    {
        animator.SetTrigger("isDying");
        Die();
    }
}