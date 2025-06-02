using UnityEngine;

public class BeerPickUp : MonoBehaviour
{
    public float healthRestoreAmount = 15f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseCharacter player = other.GetComponent<BaseCharacter>();
        if (player != null)
        {
            player.RestoreHealth(healthRestoreAmount);
            Destroy(gameObject); // Cheers! Beer consumed.
        }
    }
}
