using UnityEngine;
using Audio;
using System.Collections;

public class PlayerCharacter : BaseCharacter
{
    private bool isAttacking = false;
    [SerializeField] private PlayerAttackHitbox AttackHitBox;

    [Header("Attack Timings")]
    [SerializeField] private float hitboxStartDelay = 0.4f; // When the attack visually hits
    [SerializeField] private float hitboxDuration = 0.3f;   // How long damage is active

    private bool canAttack = true;
    private Coroutine currentAttackRoutine;

    protected override void Start()
    {
        base.Start();
        AttackHitBox = GetComponentInChildren<PlayerAttackHitbox>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && !isAttacking && canAttack && !animator.GetBool("IsAttacking"))
        {// left mouse button clicked
            currentAttackRoutine = StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        canAttack = false;

        // Set animation params
        animator.SetFloat("AttackX", lastMoveDirection.x);
        animator.SetFloat("AttackY", lastMoveDirection.y);
        animator.SetBool("isAttacking", true);
        animator.SetTrigger("Attack");

        // Wait for the wind-up before hit
        yield return new WaitForSeconds(hitboxStartDelay);

        // Play sound and enable hitbox when the attack hits
        AudioManager.Instance.PlaySFX(SFXType.PlayerAttack);
        AttackHitBox?.EnableAttack();

        // Wait while hitbox is active
        yield return new WaitForSeconds(hitboxDuration);

        // Disable hitbox
        AttackHitBox?.DisableAttack();

        // Allow next attack immediately after hitbox ends
        isAttacking = false;
        canAttack = true;
        animator.SetBool("isAttacking", false);
    }
}
