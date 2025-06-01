using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    private bool isAttacking = false;
    [SerializeField] private PlayerAttackHitbox AttackHitBox;

    protected override void Start()
    {
        base.Start();
        AttackHitBox = GetComponentInChildren<PlayerAttackHitbox>();
    }

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
        if (AttackHitBox != null)
        {
            AttackHitBox.EnableAttack(); // Allow damage during attack
        }
    }

    public void EndAttack() // Called via animation event
    {
        isAttacking = false;
        if (AttackHitBox != null)
        {
            AttackHitBox.DisableAttack(); // Stop damage after attack
        }
    }
}
