using UnityEngine;

public class EnemyController : EnemyBase
{
    public Transform player;
    protected Animator animator;

    void Update()
    {

        // If player is found, calculate direction and move toward them
        if (player != null) {
            // Calculate direction vector to player and normalize it (make it length 1)
            Vector2 direction = (player.position - transform.position).normalized;

            // Call the inherited Move method to apply movement
            Move(new Vector3(direction.x, direction.y, 0));

            // Flip the enemy's sprite based on direction (left/right)
            if (direction.x != 0) {
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
                // This mirrors the sprite depending on whether direction.x is positive or negative
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

       public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
    }
}
