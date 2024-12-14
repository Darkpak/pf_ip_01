using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5.0f;
    public float spawnDistance = 10.0f;
    public float groundOffset = 0.5f;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = Camera.main.transform;
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

        // Adjust spawn position to be on the ground
        Vector3 rayOrigin = spawnPosition + Vector3.up * 10.0f; // Start the raycast from above the spawn position
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                spawnPosition = hit.point + Vector3.up * groundOffset;
            }
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Play spawn animation
        Animator animator = enemy.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("SpawnAnimation"); // Replace "SpawnAnimation" with the name of your animation
        }
    }
}