using System;
using UnityEngine;
using DG.Tweening;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    private Gem[] gems;
    private float gemsCount;
    private Portal portal;
    private bool isPortalActive;

    [Header("Audio")]
    [SerializeField] private AudioClip activatePortalAudioClip;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        Gem.OnCreated += Gem_OnCreated;
        Gem.OnCollected += Gem_OnCollected;
        GameOver.OnGameOver += GameOver_OnGameOver;
    }

    private void OnDisable()
    {
        Gem.OnCreated -= Gem_OnCreated;
        Gem.OnCollected -= Gem_OnCollected;
        GameOver.OnGameOver -= GameOver_OnGameOver;
    }

    private void Start()
    {
        ReactivatePortal();
        FindGems();

    }

    private void Update()
    {
        if (gemsCount == 0)
        {
            ActivateFinishPortal();
        }
    }

    private void SetPortalActive(bool isActive)
    {
        portal.GetComponent<Portal>().gameObject.SetActive(isActive);
        isPortalActive = isActive;
    }

    private void FindGems()
    {
        gems = FindObjectsOfType<Gem>();
    }

    private void ActivateFinishPortal()
    {
        audioSource.PlayOneShot(activatePortalAudioClip);
        SetPortalActive(true);

    }

    private void ReactivatePortal()
    {
        portal = FindObjectOfType<Portal>();
        SetPortalActive(false);
    }

    private void Gem_OnCreated()
    {
        gemsCount++;
    }

    private void Gem_OnCollected()
    {
        gemsCount--;
    }

    private void GameOver_OnGameOver()
    {
        gemsCount = gems.Length;
        for(int i = 0; i < gems.Length; i++)
        {
            gems[i].gameObject.SetActive(true);

        }
        SetPortalActive(false);
    }
}
