using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Animator animator;
    protected float maxHealth = 100f;
    protected float currentHealth;
    public float moveSpeed = 5f;
    public GameObject coinPrefab;
    protected bool isDying = false;
    [SerializeField] protected float attackCooldown = 1f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected GoblinAttackHitbox AttackHitBox;
    protected float lastAttackTime;
    public bool playerInRange = false;

    protected Transform player;
    protected Vector3 originalScale;

    protected void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        originalScale = transform.localScale; // To keep original size

        StartCoroutine(FindPlayerAfterDelay());
    }
    IEnumerator FindPlayerAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // wait 0.5 second, not just 1 frame
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by damage taken
        if (currentHealth <= 0) // Check if health is less than or equal to 0
        {
            Die(); // Call the Die function if health is 0 or less
        }
    }

    public void Attack()
    {
        if (isDying || !playerInRange) return;

        animator.SetTrigger("GoblinAttack");

        if (AttackHitBox != null)
        {
            AttackHitBox.DamagePlayer(attackDamage); // <-- Deal 10 damage!
        }
    }

    public virtual void Die()
    {
        if (isDying) return;
        isDying = true;
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 0.12f);
    }

    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
