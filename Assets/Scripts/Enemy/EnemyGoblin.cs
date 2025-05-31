using UnityEngine;

public class EnemyGoblin : EnemyController
{
protected override void Start()
    {
        base.Start(); // Sets animator and finds player
    }

    protected override void Update()
    {
        base.Update(); // Handles movement and flipping

        if (player != null)
        {
            // Get direction to player
            Vector2 direction = (player.position - transform.position).normalized;

            // Pass movement speed to Animator to control transitions
            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            // If no player is detected, set speed to 0 to idle
            animator.SetFloat("Speed", 0f);
        }
    }
}
