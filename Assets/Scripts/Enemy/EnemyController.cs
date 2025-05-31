using UnityEngine;

public class EnemyController : EnemyBase
{
    public Transform player;
    protected Animator animator;

    protected virtual void Update()
    {

        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        Move(new Vector3(direction.x, direction.y, 0));

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        player = player ?? GameObject.FindGameObjectWithTag("Player")?.transform;
    }

       public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
    }
}
