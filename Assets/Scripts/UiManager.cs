using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverView;
    [SerializeField] private GameObject pauseView;

    private Player player;
    private bool isPaused;

    public bool IsPaused => isPaused;

    public static event Action OnRestartButton;

    private void OnEnable()
    {
        GameOver.OnGameOver += GameOver_OnGameOver;
    }

    private void OnDisable()
    {
        GameOver.OnGameOver -= GameOver_OnGameOver;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ToggleGamePauseView(false);
                Time.timeScale = 1;
                return;
            }
            ToggleGamePauseView(true);
            Time.timeScale = 0;
        }
    }

    public void ClickRestartButton()
    {
        gameOverView.SetActive(false);
        OnRestartButton?.Invoke();
    }

    public void ClickContinueButton()
    {
        ToggleGamePauseView(false);
        Time.timeScale = 1;

    }

    private void GameOver_OnGameOver()
    {
        gameOverView.SetActive(true);
    }

    private void ToggleGamePauseView(bool isActive)
    {
        pauseView.SetActive(isActive);
        isPaused = isActive;
        player.PausePlayer(isActive);
    }
}
