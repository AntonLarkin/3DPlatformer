using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Player player;

    public static event Action OnGameOver;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (player.IsPlayerDead)
        {
            OnGameOver?.Invoke();
        }
    }

}
