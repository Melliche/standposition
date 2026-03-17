using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    [SerializeField] private TMP_Text gameTimer;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private int SECOND_GAME_DURATION = 600;
    [SerializeField] private GameObject canvaMenu;

    private float elapsedTime = 0f;
    private int lastDisplayedSecond = -1;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        UpdateTimer();
    }

    /// <summary>
    /// Affiche le panneau de fin de jeu avec le message "Game Over" et arrête le jeu.
    /// </summary>
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over";
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Recharge la scène actuelle pour recommencer la partie
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Charge la scène du menu principal
    /// </summary>
    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Met le jeu en pause, affiche le menu pause
    /// </summary>
    public void TogglePause()
    {
        bool paused = pausePanel.activeSelf;

        pausePanel.SetActive(!paused);
        Time.timeScale = paused ? 1 : 0;
    }

    
    /// <summary>
    /// Met à jour le timer de jeu en fonction du temps écoulé depuis le début de la partie.
    /// Affiche le temps au format MM:SS et vérifie si la durée maximale de la partie est atteinte pour déclencher la fin du jeu.
    /// </summary>
    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;

        int totalSeconds = Mathf.FloorToInt(elapsedTime);

        if (totalSeconds != lastDisplayedSecond)
        {
            lastDisplayedSecond = totalSeconds;

            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            gameTimer.text = $"{minutes:00}:{seconds:00}";
        }

        if (elapsedTime >= SECOND_GAME_DURATION)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Victory !";
            Time.timeScale = 0f;

        }
    }
}