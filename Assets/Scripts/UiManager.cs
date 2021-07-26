using System;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverView;

    public static event Action OnRestartButton;

    private void OnEnable()
    {
        GameOver.OnGameOver += GameOver_OnGameOver;
    }

    private void OnDisable()
    {
        GameOver.OnGameOver -= GameOver_OnGameOver;
    }

    public void ClickRestartButton()
    {
        gameOverView.SetActive(false);

        OnRestartButton?.Invoke();
    }

    private void GameOver_OnGameOver()
    {
        gameOverView.SetActive(true);
    }

}
