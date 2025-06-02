using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1; // how much this coin is worth (can expand later)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");

            // TODO: Add to player's coin count (optional, later)

            Destroy(gameObject); // Destroy the coin after pickup
        }
    }
}

