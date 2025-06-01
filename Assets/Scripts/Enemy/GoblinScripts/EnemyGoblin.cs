using UnityEngine;

public class EnemyGoblin : EnemyController
{
    private Transform playerTransform;
    private int attackCount = 0;
    public float stopDistance = 1.2f;
   

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
        animator.SetTrigger("GoblinDeath");
        Die(); // internally call the protected one
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit enemy!");
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
