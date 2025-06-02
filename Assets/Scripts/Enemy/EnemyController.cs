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
            return;
    }

    Vector3 targetPosition = player.position;
    Vector2 toPlayer = targetPosition - transform.position;

    float distance = toPlayer.magnitude;

    if (distance > 0.5f)
    {
        Vector2 direction = toPlayer.normalized;
        Move(new Vector3(direction.x, direction.y, 0));

        animator.SetFloat("Speed", moveSpeed); // Set positive speed for walking animation

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(direction.x), originalScale.y, originalScale.z);
        }
    }
    else
    {
        Move(Vector3.zero);
        animator.SetFloat("Speed", 0f); // Idle animation
    }
}


    public override void Die()
    {
        if (isDying) return;

        base.Die();  // disable collider, healthbar, etc.

        animator.SetTrigger(DeathTriggerName);

        StartCoroutine(WaitForDeathAnimation());
    }


    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(0.1f); // small delay for animator transition

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until death animation is playing
        while (!stateInfo.IsName(DeathTriggerName))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait until animation finishes playing once (normalizedTime >= 1 means full playthrough)
        while (stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Now destroy the enemy
        Destroy(gameObject);
    }

}
