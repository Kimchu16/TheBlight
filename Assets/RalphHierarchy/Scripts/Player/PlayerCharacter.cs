using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    private bool isAttacking = false;
    [SerializeField] private PlayerAttackHitbox AttackHitBox;
    [SerializeField] private float attackDuration = 0.2f;

    protected override void Start()
    {
        base.Start();
        AttackHitBox = GetComponentInChildren<PlayerAttackHitbox>();
    }

    protected override void Update()
    {
       
        base.Update();

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
        Invoke(nameof(EndAttack), attackDuration);
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
