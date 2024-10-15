using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnInterval = 2f; // Minimum time between each obstacle spawn
    [SerializeField] private float maxSpawnInterval = 6f; // Maximum time between each obstacle spawn
    [SerializeField] private float spawnInterval = 2f; // Time between each obstacle spawn
    [SerializeField] private ObstaclePool obstaclePool; // The obstacle pool in the scene

    private Coroutine spawnCoroutine; // To keep track of the spawn coroutine

    private IEnumerator SpawnObstacle()
    {
        // Loop indefinitely to keep spawning obstacles
        while (true) 
        {
            // Get an obstacle from the pool
            GameObject obstacle = obstaclePool.GetObstacle(); 
            
            // Set its position to a random point 
            float randomPosition = Random.Range(-3.4f, 3.4f);
            obstacle.transform.position = new Vector3(transform.position.x, randomPosition, transform.position.z); 
            
            // Wait for a random time before spawning the next obstacle
            float spawnDelay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            // Stop the spawning coroutine
            StopCoroutine(spawnCoroutine); 
            
            // Clear the reference
            spawnCoroutine = null; 
        }

        // Stop all active obstacles
        obstaclePool.ReturnAllObstacles();
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            // Start the spawning coroutine
            spawnCoroutine = StartCoroutine(SpawnObstacle()); 
        }
    }
}

