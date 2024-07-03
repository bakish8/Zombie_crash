using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public Transform targetPoint;
    public float spawnInterval = 2f;
    public int maxZombies = 10;

    private List<GameObject> activeZombies = new List<GameObject>();
    private float initialHeight; // Store the initial height

    private void Start()
    {
        // Store the initial height based on the first zombie's spawn point
        initialHeight = leftSpawnPoint.position.y ;
        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Clean up the list by removing null entries (destroyed zombies)
            activeZombies.RemoveAll(zombie => zombie == null);

            // Check if the number of active zombies exceeds the max limit
            if (activeZombies.Count >= maxZombies)
            {
                continue; // Skip this spawn cycle
            }

            // Randomly choose between left and right spawn points
            bool spawnFromLeft = Random.Range(0, 2) == 0;
            Transform spawnPoint = spawnFromLeft ? leftSpawnPoint : rightSpawnPoint;

            // Instantiate a new zombie at the chosen spawn point with the same initial height
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, initialHeight, -1); // Set Z position to -1 explicitly
            spawnPosition.z = -2; // Explicitly set the Z position to -1
            GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // Flip the zombie if it spawns from the right
            if (!spawnFromLeft)
            {
                SpriteRenderer spriteRenderer = newZombie.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = true;
                }
            }

            // Add the new zombie to the active zombies list
            activeZombies.Add(newZombie);

            // Set the target point for the zombie
            enemyPatrol patrolScript = newZombie.GetComponent<enemyPatrol>();
            if (patrolScript != null)
            {
                patrolScript.currentPoint = targetPoint;
                Debug.Log($"Assigned currentPoint for the new zombie to {targetPoint.position}");
            }
            else
            {
                Debug.LogError("No enemyPatrol script found on the new zombie.");
            }
        }
    }
}
