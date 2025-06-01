using UnityEngine;

public class EnemyGoblin : EnemyController
{
    public float attackCooldown = 1f;  // Attack every 1 second
    private float lastAttackTime;
    private bool playerInRange = false; // <-- New
    private Transform playerTransform;
    private int attackCount = 0;
    public float stopDistance = 1.2f;

    protected override void Start()
    {
        base.Start(); // Sets animator and finds player
    }

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

    private void Attack()
    {
        if (isDying || !playerInRange) return;

        animator.SetTrigger("GoblinAttack");

        Debug.Log("Goblin swings! Attack #" + (++attackCount));

        // Later, here you can call Player.TakeDamage(10);
    }

    protected override void Die()
    {
        if (isDying) return;
        isDying = true;
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        animator.SetTrigger("GoblinDeath");
        Destroy(gameObject, 0.12f);
    }

    public void KillGoblin()
    {
        Die(); // internally call the protected one
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = other.transform;

            attackCount = 0; // <-- Reset attack counter when player enters range
            Debug.Log("Player entered range! Starting attack sequence.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;

            Debug.Log("Player left range. Total attacks made: " + attackCount);
            animator.SetFloat("Speed", 0f);
        }
    }


}
