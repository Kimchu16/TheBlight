using UnityEngine;
using System.Collections; 

public class EnemyController : EnemyBase
{
    public Transform player;
    protected Animator animator;
    protected bool isDying = false;
    private Vector3 originalScale;
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

    IEnumerator FindPlayerAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // wait 0.5 second, not just 1 frame
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        originalScale = transform.localScale; // To keep original size

        StartCoroutine(FindPlayerAfterDelay());
    }



    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        if (isDying) return;
        isDying = true;
        animator.SetTrigger("isDying");
    }


}
