using UnityEngine;
using Audio;

public class PlayerCharacter : BaseCharacter
{
    private bool isAttacking = false;
    [SerializeField] private PlayerAttackHitbox AttackHitBox;
    [SerializeField] private float attackDuration = 0.1f;
    [SerializeField] private float attackCooldown = 1f; 
    private bool canAttack = true;

    protected override void Start()
    {
        base.Start();
        AttackHitBox = GetComponentInChildren<PlayerAttackHitbox>();
    }

    protected override void Update()
    {

        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking && canAttack)
        {
            Attack();
        }

    }

    private void Attack()
    {
        canAttack = false;
        isAttacking = true;

        animator.SetFloat("AttackX", lastMoveDirection.x);
        animator.SetFloat("AttackY", lastMoveDirection.y);
        animator.SetTrigger("Attack");

        PlayAttackSound();

        if (AttackHitBox != null)
        {
            AttackHitBox.EnableAttack(); // Allow damage during attack
        }
        Invoke(nameof(EndAttack), attackDuration);    
        Invoke(nameof(ResetAttackCooldown), attackCooldown);
    }
    private void ResetAttackCooldown()
    {
        canAttack = true;
    }

    public void EndAttack() // Called via animation event
    {
        isAttacking = false;
        if (AttackHitBox != null)
        {
            AttackHitBox.DisableAttack(); // Stop damage after attack
        }
    }

    private void PlayAttackSound()
    {
        AudioManager.Instance.PlaySFX(SFXType.PlayerAttack);
    }
}
