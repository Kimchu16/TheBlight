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
        if (isDying) return; // prevent double-die
        isDying = true;
        Transform healthBar = transform.Find("HealthBar"); // or whatever the name of healthbar GameObject is
        if (healthBar != null)
        {
        healthBar.gameObject.SetActive(false);
        }

        animator.SetTrigger(DeathTriggerName); // Only trigger Death Animation!

        // Start a coroutine to wait for the animation to finish
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Small delay to ensure Animator has transitioned
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until we're playing the death animation
        while (!stateInfo.IsName(DeathTriggerName))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        float waitTime = stateInfo.length;
        Debug.Log($"Death animation length: {waitTime}");

        // Wait for death animation to finish
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }


}
