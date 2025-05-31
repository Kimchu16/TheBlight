using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    protected Animator animator;
    protected Vector2 movement;
    protected Vector2 lastMoveDirection = Vector2.down;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        // Get input axis (Horizontal = A/D/LeftRight, Vertical = W/S/UpDown)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        if (movement.magnitude > 0.01f)
        {
            lastMoveDirection = movement;
        }

        Move(movement);
        Animate(movement);
    }

    // Move player based on input
    protected virtual void Move(Vector2 direction)
    {
        Vector3 moveDir = new Vector3(direction.x, direction.y, 0);
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    // Send movement data to animator for blending
    protected virtual void Animate(Vector2 direction)
    {
        float speed = direction.magnitude;
        animator.SetBool("IsMoving", speed > 0.01f);
        animator.SetFloat("Speed", speed);

        if (speed > 0.01f)
        {
            float moveX = Mathf.Round(direction.x);
            float moveY = Mathf.Round(direction.y);

            // Prioritize main axis for 4-direction movement
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                moveY = 0;
            }
            else
            {
                moveX = 0;
            }

            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);

            lastMoveDirection = new Vector2(moveX, moveY);
        }

        animator.SetFloat("IdleX", lastMoveDirection.x);
        animator.SetFloat("IdleY", lastMoveDirection.y);
    }
}
