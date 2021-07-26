using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticle;

    [Header("Audio")]
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioSource audioSource;

    private bool isPlayerDead;

    public bool IsPlayerDead => isPlayerDead;


    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        GameOver.OnGameOver += GameOver_OnGameOver;
        UiManager.OnRestartButton += UiManager_OnRestartButton;
    }

    private void OnDisable()
    {
        GameOver.OnGameOver -= GameOver_OnGameOver;
        UiManager.OnRestartButton -= UiManager_OnRestartButton;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Ground))
        {
            SetPlayerDead();
        }
    }

    public void SetPlayerDead()
    {
        audioSource.PlayOneShot(deathAudioClip);
        isPlayerDead = true;
        Instantiate(deathParticle,transform);
    }

    private void GameOver_OnGameOver()
    {
        if (isPlayerDead)
        {
            transform.position = startPosition;
        }
    }

    private void UiManager_OnRestartButton()
    {
        isPlayerDead = false;
    }


}
