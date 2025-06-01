using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    private bool isAttacking = false;

    protected override void Update()
    {
        if (!isAttacking)
        {
            base.Update();
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

    public void EndAttack() // Called via animation event
    {
        isAttacking = false;
    }
}
