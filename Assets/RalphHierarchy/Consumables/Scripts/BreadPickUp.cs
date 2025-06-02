using UnityEngine;

public class BreadPickUp : MonoBehaviour
{
    public float hungerRestoreAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseCharacter player = other.GetComponent<BaseCharacter>();
        if (player != null)
        {
            player.RestoreHunger(hungerRestoreAmount);
            Destroy(gameObject); // Poof! Bread gone.
        }
    }
}