using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private Animator animatorClouds;
    [SerializeField] private Animator animatorGround;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private bool _isActive;
    private float _score;
    private int _highScore;
    private SortedSet<int> _topScores = new SortedSet<int>();
    
    private void Update()
    {
        // Update score
        if (_isActive)
        {
            _score += 0.005f;
            UpdateScoreText();
        }
    }
    
    public void StartGame()
    {
        _isActive = true;
        
        // Load scores at the start
        LoadScores(); 
        
        // Init score
        _highScore = _topScores.Max;
        UpdateHighScoreText();
        
        _score = 0;
        UpdateScoreText();
        
        // Init animation
        animatorClouds.SetBool("IsActive", true);
        animatorGround.SetBool("IsActive", true);
        
        // Start game logic
        playerController.StartPlayer(); // Ensure the player starts in the correct state
        obstacleSpawner.StartSpawning(); // Start spawning obstacles
    }
    
    public void GameOver()
    {
        string message = "Game Over!";
        _isActive = false;
        
        // Add sound
        AudioManager.Instance.StopSound();
        AudioManager.Instance.PlayExplosion();
        
        // Handle end game logic
        playerController.StopPlayer(); // Stop player movement
        obstacleSpawner.StopSpawning(); // Stop spawning obstacles
        
        animatorClouds.SetBool("IsActive", false);
        animatorGround.SetBool("IsActive", false);

        // Check if the player won (Set a record)
        if (_highScore < _score)
        {
            message = "You Won!";
        }
        
        // Save high score
        AddScoreToHeap();
        
        // Show game over menu
        MenuManager.Instance.ShowGameOverMenu(Mathf.RoundToInt(_score), message); 
    }
    
    private void AddScoreToHeap()
    {
        // Add the score to the sorted set (min-heap behavior)
        _topScores.Add(Mathf.RoundToInt(_score));

        // If there are more than 5 scores, remove the smallest one
        if (_topScores.Count > 5)
        {
            _topScores.Remove(_topScores.Min); // Remove the minimum score
        }

        // Save top scores to PlayerPrefs
        SaveScores();
    }

    private void LoadScores()
    {
        // Clear the current scores
        _topScores.Clear();

        // Load scores from PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("TopScore" + i))
            {
                _topScores.Add(PlayerPrefs.GetInt("TopScore" + i));
            }
        }
    }

    private void SaveScores()
    {
        // Save the top scores to PlayerPrefs
        int index = 0;
        foreach (int score in _topScores)
        {
            PlayerPrefs.SetInt("TopScore" + index, score);
            index++;
        }
        
        // Ensure the data is saved
        PlayerPrefs.Save(); 
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {Mathf.RoundToInt(_score)}";
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score: {_highScore}";
    }
    
}
