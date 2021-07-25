using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticle;

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
    }

    private void OnDisable()
    {
        GameOver.OnGameOver -= GameOver_OnGameOver;
    }

    public void SetPlayerDead()
    {
        isPlayerDead = true;
        Instantiate(deathParticle,transform);
        transform.position = startPosition;
    }

    private void GameOver_OnGameOver()
    {
        StartCoroutine(OnDead());
    }

    private IEnumerator OnDead()
    {
        yield return new WaitForSeconds(1f);        
        isPlayerDead = false;
    }

}
