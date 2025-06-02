using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign Your Player Prefab")]
    public GameObject playerPrefab;

    [Header("Optional Default Spawn Point (used if no saved position)")]
    public Transform defaultSpawnPoint;

    private const string SpawnXKey = "PlayerSpawnX";
    private const string SpawnYKey = "PlayerSpawnY";

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("PlayerSpawner: No player prefab assigned!");
            return;
        }

        Vector3 spawnPosition;

        if (PlayerPrefs.HasKey(SpawnXKey) && PlayerPrefs.HasKey(SpawnYKey))
        {
            float x = PlayerPrefs.GetFloat(SpawnXKey);
            float y = PlayerPrefs.GetFloat(SpawnYKey);
            spawnPosition = new Vector3(x, y, 0f);
        }
        else
        {
            spawnPosition = defaultSpawnPoint ? defaultSpawnPoint.position : Vector3.zero;
        }

        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

    // Call this from player scripts or checkpoints to update saved position
    public static void SaveSpawnPoint(Vector2 newPosition)
    {
        PlayerPrefs.SetFloat(SpawnXKey, newPosition.x);
        PlayerPrefs.SetFloat(SpawnYKey, newPosition.y);
        PlayerPrefs.Save();
    }
}
