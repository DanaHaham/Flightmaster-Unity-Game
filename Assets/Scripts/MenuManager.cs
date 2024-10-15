using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameManagerCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text recordsText;
    
    private void Start()
    {
        ShowMainMenu();
    }

    // Start the game
    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameManagerCanvas.SetActive(true);
        GameManager.Instance.StartGame(); // Start game logic
    }

    // Exit
    public void QuitGame()
    {
        Application.Quit();
    }

    // Init the Game Over screen
    public void ShowGameOverMenu(int score, string message)
    {
        mainMenuCanvas.SetActive(false);
        gameManagerCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        scoreText.text = $"Your Score: {score}";
        messageText.text = message;
    }

    // Init the Main Menu screen
    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameManagerCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        UpdateRecordsText();
    }

    private void UpdateRecordsText()
    {
        string records = "Top 5: \n";
        
        // Load scores from PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("TopScore" + i))
            {
                records += $"Number {i+1}: {(PlayerPrefs.GetInt("TopScore" + (4-i)))} \n";
            }
        }
        
        recordsText.text = records;
    }

}
