using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f; 
    [SerializeField] private Sprite[] obstacleSprites;
    
    private Vector2 _screenBounds;
    private ObstaclePool _pool;

    private void Start()
    {
        // Get the screen bounds, so we can destroy the obstacle when it's off-screen
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        // Assign a random sprite to the obstacle when it spawns
        AssignRandomSprite();
    }

    private void Update()
    {
        // Move the obstacle to the left
        transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));

        // Destroy the obstacle when it goes off the left side of the screen
        if (transform.position.x < -_screenBounds.x * 2)
        {
            ObstaclePool _pool = FindObjectOfType<ObstaclePool>();
            
            // Return it to the pool
            _pool.ReturnObstacle(gameObject); 
        }
    }
    
    private void AssignRandomSprite()
    {
        // Choose a random sprite from the array
        int randomIndex = Random.Range(0, obstacleSprites.Length);

        // Assign the random sprite to the SpriteRenderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = obstacleSprites[randomIndex];
            transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            boxCollider.size = spriteRenderer.bounds.size;
        }
    }
}
