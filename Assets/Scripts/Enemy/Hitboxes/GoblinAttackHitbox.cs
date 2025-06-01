using UnityEngine;

public class GoblinAttackHitbox : MonoBehaviour
{
    private PlayerCharacter player; // Store reference to Player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerCharacter>(); // cache the player
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

    // Call this when goblin is swinging
    public void DamagePlayer(float damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}

