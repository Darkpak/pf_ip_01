using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPrefab; // The ammo collectible prefab
    public float spawnInterval = 10.0f; // Time interval between spawns
    public float spawnRadius = 20.0f; // Radius within which to spawn ammo
    public LayerMask groundLayer; // Layer mask to identify ground

    private void Start()
    {
        InvokeRepeating("SpawnAmmo", 0, spawnInterval);
    }

    private void SpawnAmmo()
    {
        Vector3 spawnPosition = GetRandomGroundPosition();
        if (spawnPosition != Vector3.zero)
        {
            Quaternion spawnRotation = Quaternion.Euler(-90, 0, 0);
            Instantiate(ammoPrefab, spawnPosition, spawnRotation);
        }
    }

    private Vector3 GetRandomGroundPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            10.0f, // Start the raycast from above the ground
            Random.Range(-spawnRadius, spawnRadius)
        );

        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return hit.point;
            }
        }

        return Vector3.zero; // Return zero vector if no valid ground position is found
    }
}