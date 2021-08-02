using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticle;

    [Header("Audio")]
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioSource audioSource;

    private bool isBusy;
    private bool isPaused;
    private bool isPlayerDead;

    public bool IsPlayerDead => isPlayerDead;
    public bool IsBusy => isBusy;
    public bool IsPaused => isPaused;


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

    private void Update()
    {
        if (isBusy)
        {
            transform.Translate(Vector3.up * 0.01f);
        }
    }

    public void PausePlayer(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public void SetPlayerDead()
    {
        audioSource.PlayOneShot(deathAudioClip);
        isPlayerDead = true;
        Instantiate(deathParticle,transform);
    }

    public void TeleportPlayer(Vector3 destinationPortalPosition)
    {
        isBusy = true;
        StartCoroutine(OnTeleport(destinationPortalPosition));
    }

    private IEnumerator OnTeleport(Vector3 destinationPortalPosition)
    {
        yield return new WaitForSeconds(1.5f);

        transform.DOMove(destinationPortalPosition, 0.1f);
        isBusy = false;
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
