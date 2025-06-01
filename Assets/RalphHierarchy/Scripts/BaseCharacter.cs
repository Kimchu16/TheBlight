using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 7.5f;
    protected float currentMoveSpeed;

    protected Animator animator;
    protected Vector2 movement;
    protected Vector2 lastMoveDirection = Vector2.down;

    // Health
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;

    // Stamina
    public float maxStamina = 10f;
    public float currentStamina;
    public float staminaDrainRate = 0.5f;
    public float staminaRegenRate = 1.5f;
    public Image StaminaFill;

    private bool isRestingInPeace = false;
    private bool isRunning = false;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMoveSpeed = walkSpeed;

        UpdateHealthBar();
        UpdateStaminaBar();
    }

    protected virtual void Update()
    {
        HandleInput();
        HandleStamina();
        if (isRestingInPeace)
        {
            Animate(); // Optional: could add rest animation here
            return;
        }

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

    void HandleStamina()
    {
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        isRunning = wantsToRun && movement.sqrMagnitude > 0.01f && currentStamina > 0f;

        if (isRunning)
        {
            currentMoveSpeed = runSpeed;
            currentStamina -= staminaDrainRate * Time.deltaTime;

            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isRestingInPeace = true;
            }
        }
        else
        {
            currentMoveSpeed = walkSpeed;

            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;

                if (currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                    isRestingInPeace = false;
                }
            }
        }

        UpdateStaminaBar();
    }

    void Move()
    {
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime);
    }

    void Animate()
    {
        bool isMoving = movement.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", movement.magnitude);

        Vector2 animDir = lastMoveDirection;

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

    void UpdateStaminaBar()
    {
        if (StaminaFill != null)
        {
            StaminaFill.fillAmount = currentStamina / maxStamina;
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        // TODO: Disable controls, play death animation, etc.
    }
}
