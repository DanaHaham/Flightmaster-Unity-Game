using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float maxLiftVelocity = 5f; 
    
    private bool _isFlying; 
    private bool _isActive; 
    
    private void Awake()
    {
        StopPlayer();
    }
    
    private void Update()
    {
        if (_isActive)
        {
            HandleThrust();
            HandleRotation();
        }
    }

    // Check for player input and apply thrust force
    private void HandleThrust()
    { 
        // Check if the left mouse button or space bar is being pressed
        _isFlying = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

        if (_isFlying)
        {
            // Add force
           if (rb.velocity.y < maxLiftVelocity)
           {
               rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);
           }
           
           // Play audio
           AudioManager.Instance.PlayHelicopter();
        }
        else
        {
            // Stop audio
            AudioManager.Instance.StopSound();
        }
    }
    
    // Rotate the helicopter for a feel of flying
    private void HandleRotation()
    {
        if (rb.velocity.y > 0)
        {
            // Rotate upwards when flying
            rb.rotation = Mathf.LerpAngle(rb.rotation, 5f, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Rotate downwards when falling
            rb.rotation = Mathf.LerpAngle(rb.rotation, -5f, Time.deltaTime * rotationSpeed);
        }
    }
    
    // Check collision 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Clouds"))
        {
            // Trigger game over
            GameManager.Instance.GameOver(); 
        }
    }
    
    // Start player state
    public void StartPlayer()
    {
        transform.position = new Vector3(-5f, 0, 0);
        rb.isKinematic = false;
        _isActive = true;
    }

    // Stop player state
    public void StopPlayer()
    {
        transform.position = new Vector3(-5f, 0, 0);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        _isActive = false;
    }
}
