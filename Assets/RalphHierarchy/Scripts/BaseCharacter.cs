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

        // Update lastMoveDirection only if moving
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
            // Only update MoveX/MoveY when actually moving
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            lastMoveDirection = direction;
        }

        animator.SetFloat("IdleX", Mathf.Round(lastMoveDirection.x));
        animator.SetFloat("IdleY", Mathf.Round(lastMoveDirection.y));

    }

}
