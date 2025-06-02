using UnityEngine;

public abstract class EnemyAttackHitbox : MonoBehaviour
{
    protected PlayerCharacter player;

    protected virtual float DamageMultiplier => 1f; // default 1x

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerCharacter>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null && other.gameObject == player.gameObject)
            {
                player = null;
            }
        }
    }

    public void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount * DamageMultiplier);
        }
    }
}
