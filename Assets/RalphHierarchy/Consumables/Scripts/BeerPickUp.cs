using UnityEngine;

public class BeerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddBeer();  // Increment beer in inventory
            Destroy(gameObject); // Destroy beer object after pickup
        }
    }
}
