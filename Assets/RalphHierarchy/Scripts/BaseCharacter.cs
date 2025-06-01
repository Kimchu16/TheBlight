using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;

    protected Animator animator;
    protected Vector2 movement;
    protected Vector2 lastMoveDirection = Vector2.down;

    //health
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    protected virtual void Update()
    {
        HandleInput();
        Move();
        Animate();
    }

    void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized;

        if (movement.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = movement;
        }
    }

    void Move()
    {
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void Animate()
    {
        bool isMoving = movement.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", movement.magnitude);

        Vector2 animDir = lastMoveDirection;

        // Snap to 4-direction movement
        if (isMoving)
        {
            animDir = Mathf.Abs(movement.x) > Mathf.Abs(movement.y)
                ? new Vector2(Mathf.Sign(movement.x), 0)
                : new Vector2(0, Mathf.Sign(movement.y));

            lastMoveDirection = animDir;
        }

        animator.SetFloat("MoveX", animDir.x);
        animator.SetFloat("MoveY", animDir.y);
        animator.SetFloat("IdleX", lastMoveDirection.x);
        animator.SetFloat("IdleY", lastMoveDirection.y);
    }

    //health 
    // Updates health bar fill amount (0-1)
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        // TODO: Add death animation, disable player controls, etc.
    }
}
