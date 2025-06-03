using UnityEngine;

public class BreadPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddBread();  // Increment bread in inventory
            Destroy(gameObject); // Destroy bread object after pickup
        }
    }
}