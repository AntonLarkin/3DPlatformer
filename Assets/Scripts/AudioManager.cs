using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    private AudioClip clip;
    private AudioSource audioSource;
    private float volumeLevel;

    private bool isReadyToChangeClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();

        StartCoroutine(OnChangeMusic());
    }

    private void Update()
    {
        if (isReadyToChangeClip)
        {
            StartCoroutine(OnChangeMusic());
            isReadyToChangeClip = false;
        }

        audioSource.volume = volumeLevel;
    }

    public void SetVolumeLevel(float vol)
    {
        volumeLevel = vol;
    }


    private IEnumerator OnChangeMusic()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        do
        {
            clip = clips[Random.Range(0, clips.Length)];
        }
        while (clip == audioSource.clip);

        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(OnReadyToChange());
    }

    private IEnumerator OnReadyToChange()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        isReadyToChangeClip = true;
    }

}
