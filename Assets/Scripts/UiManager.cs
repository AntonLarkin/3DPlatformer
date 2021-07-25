using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverView;

    private void OnEnable()
    {
        GameOver.OnGameOver += GameOver_OnGameOver;
    }

    private void OnDisable()
    {
        GameOver.OnGameOver -= GameOver_OnGameOver;
    }

    public void OnRestartButtonClicked()
    {
        gameOverView.SetActive(false);
    }

    private void GameOver_OnGameOver()
    {
        gameOverView.SetActive(true);
    }

}
