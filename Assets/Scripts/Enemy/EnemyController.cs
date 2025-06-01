using UnityEngine;
using System.Collections;

public class EnemyController : EnemyBase
{

    protected virtual void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                return; // no player still
        }
        Vector3 targetPosition = player.position;
        Vector2 toPlayer = targetPosition - transform.position;

        float distance = toPlayer.magnitude; // <-- Distance between goblin and player

        if (distance > 0.5f) // <-- Only move if far away
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
            // Within attack range, don't move (idle or attack)
            Move(Vector3.zero); // Optional: you can stop movement cleanly
        }
    }

    public override void Die()
    {
        animator.SetTrigger("isDying");
        animator.SetTrigger(DeathTriggerName);
        Destroy(gameObject, 1f);
        
    }


}
