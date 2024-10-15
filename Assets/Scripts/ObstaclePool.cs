using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab; 
    [SerializeField] private int poolSize = 7; 
    private List<GameObject> _pool;

    void Start()
    {
        // Initialize the pool
        _pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            
            // Start with all obstacles disabled
            obj.SetActive(false); 
            _pool.Add(obj);
        }
    }

    // Get an obstacle from the pool
    public GameObject GetObstacle()
    {
        foreach (GameObject obj in _pool)
        {
            // Find an inactive obstacle
            if (!obj.activeInHierarchy) 
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive obstacle is found, create a new one
        GameObject newObj = Instantiate(obstaclePrefab);
        newObj.SetActive(true);
        _pool.Add(newObj);
        return newObj;
    }

    // Return the obstacle to the pool
    public void ReturnObstacle(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    // Stop all active obstacles
    public void ReturnAllObstacles()
    {
        foreach (GameObject obj in _pool)
        {
            ReturnObstacle(obj);
        }
    }
}
