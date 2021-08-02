using UnityEngine;
using DG.Tweening;

public class MovableObject : MonoBehaviour
{
    [SerializeField] private float timeDelay;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float liftTime;
    [SerializeField] private bool isNeedToBeReturned;

    [Header("Audio")]
    [SerializeField] private AudioClip moveAudioClip;
    [SerializeField] private AudioSource audioSource;

    public Vector3 EndPosition => endPosition;
    public Vector3 StartPosition => startPosition;
    public bool IsNeedToBeReturned => isNeedToBeReturned;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player)) 
        {
            other.GetComponent<Player>().SetPlayerDead();
            return;
        }
    }

    public void LiftBox(Vector3 position)
    {
        audioSource.PlayOneShot(moveAudioClip);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(timeDelay);
        transform.DOShakePosition(timeDelay,0.2f,90);
        sequence.Append(transform.DOLocalMove(position, liftTime));
    }

    private void GameOver_OnGameOver()
    {
        gameObject.transform.position = startPosition;
        //DOTween.KillAll();
    }

}
