using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    private Gem[] gems;
    private Portal portal;
    private bool isPortalActive;

    [Header("Audio")]
    [SerializeField] private AudioClip activatePortalAudioClip;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        ReactivatePortal();
    }

    private void OnEnable()
    {
        UiManager.OnRestartButton += UiManager_OnRestartButton;
    }

    private void OnDisable()
    {
        UiManager.OnRestartButton -= UiManager_OnRestartButton;
    }


    private void Update()
    {
        FindGems();
        CheckForGems();
        if (isPortalActive&&gems.Length>0)
        {
            ReactivatePortal();
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

    private void CheckForGems()
    {
        if (gems.Length==0)
        {
            audioSource.PlayOneShot(activatePortalAudioClip);
            SetPortalActive(true);
        }
    }

    private void ReactivatePortal()
    {
        portal = FindObjectOfType<Portal>();
        SetPortalActive(false);
    }

    private void UiManager_OnRestartButton()
    {

    }

}
