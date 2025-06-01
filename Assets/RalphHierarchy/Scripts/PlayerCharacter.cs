using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    private bool isAttacking = false;

    protected override void Update()
    {
        if (!isAttacking)
        {
            base.Update(); // Handle movement and animation
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetFloat("AttackX", lastMoveDirection.x);
        animator.SetFloat("AttackY", lastMoveDirection.y);
        animator.SetTrigger("Attack");
    }

    // Called via animation event at the end of the attack animation
    public void EndAttack()
    {
        isAttacking = false;
    }
}
