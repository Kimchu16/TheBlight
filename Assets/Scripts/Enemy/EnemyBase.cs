using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Animator animator;
    protected float maxHealth = 100f;
    protected float currentHealth;
    public float moveSpeed = 5f;
    public GameObject coinPrefab;
    public bool isDying = false;
    [SerializeField] protected float attackCooldown = 1f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected EnemyAttackHitbox AttackHitBox;
    protected virtual string AttackTriggerName => "GoblinAttack";
    protected virtual string DeathTriggerName => "GoblinDeath";
    protected float lastAttackTime;
    public bool playerInRange = false;

    protected Transform player;
    protected Vector3 originalScale;

    protected Transform playerTransform;
    protected int attackCount = 0;

    protected bool isChasing = false;

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

    public virtual void Attack()
    {
        if (isDying || !playerInRange) return;

        animator.SetTrigger(AttackTriggerName);

        if (AttackHitBox != null)
        {
            AttackHitBox.DamagePlayer(attackDamage); // <-- Deal 10 damage!
        }
    }

    public virtual void Die()
    {
        if (isDying) return;
        isDying = true;

        animator.SetTrigger(DeathTriggerName);

        Transform healthBar = transform.Find("HealthBar");
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject); // Destroy it completely instead of just SetActive(false)
        }

        if (AttackHitBox != null)
            AttackHitBox.gameObject.SetActive(false);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }
    protected virtual void OnDeathComplete() { }

    public virtual void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            isChasing = true;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            isChasing = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
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

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;

            Debug.Log("Player left range. Total attacks made: " + attackCount);
            animator.SetFloat("Speed", 0f);
        }
    }

    protected bool IsChasing()
    {
        return isChasing;
    }
}
