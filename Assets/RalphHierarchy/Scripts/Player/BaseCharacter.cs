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

    public float maxHunger = 100f;
    public float currentHunger;
    public Image hungerBarFill;

    [SerializeField] private float hungerDamagePerDrain = 2f;
    [SerializeField] private float hungerDrainAmount = 5f;
    [SerializeField] private float hungerDrainInterval = 10f;
    private float healthDrainTimer = 0f;

    private bool isRestingInPeace = false;
    private bool isRunning = false;

    [SerializeField] private string deathTriggerName = "PlayerDie"; // Animator Trigger Name
    private bool isDead = false; // Prevent double death

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentHunger = maxHunger;
        currentMoveSpeed = walkSpeed;

        UpdateHealthBar();
        UpdateStaminaBar();
        UpdateHungerBar();
    }

    protected virtual void Update()
    {
        HandleInput();
        HandleStamina();
        HandleHungerDrain();
        HandleItemUse();
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

    void HandleHungerDrain()
    {
        healthDrainTimer += Time.deltaTime;
        if (healthDrainTimer >= hungerDrainInterval)
        {
            healthDrainTimer = 0f;

            // Reduce hunger over time
            currentHunger -= hungerDrainAmount;
            currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
            UpdateHungerBar();

            // If hunger reaches 0, start damaging health slowly
            if (currentHunger <= 0f)
            {
                TakeDamage(hungerDamagePerDrain);
            }
        }
    }
    void HandleItemUse()
    {
        // Press 1 -> Eat Bread
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 key
        {
            if (InventoryManager.Instance.UseBread())
            {
                RestoreHunger(20f); // Heal hunger by 20
                Debug.Log("Ate bread!");
            }
            else
            {
                Debug.Log("No bread to eat!");
            }
        }

        // Press 2 -> Drink Beer
        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 key
        {
            if (InventoryManager.Instance.UseBeer())
            {
                RestoreHealth(15f); // Heal health by 15
                Debug.Log("Drank beer!");
            }
            else
            {
                Debug.Log("No beer to drink!");
            }
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar();
    }

    public void RestoreHunger(float amount)
    {
        currentHunger = Mathf.Min(currentHunger + amount, maxHunger);
        UpdateHungerBar();
    }
    void UpdateHungerBar()
    {
        if (hungerBarFill != null)
        {
            hungerBarFill.fillAmount = currentHunger / maxHunger;
        }
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
        if (isDead)
        {
            return;
        }

        isDead = true;

        Debug.Log($"{gameObject.name} died.");
        animator.SetTrigger(deathTriggerName); // Play death animation

        // Disable further inputs/movement
        this.enabled = false; // Disables Update() from this point

        // Optional: Disable colliders, attack scripts, etc.
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        float deathAnimationLength = 1.7f; // <- adjust based on your animation
        Destroy(gameObject, deathAnimationLength);
    }
}
