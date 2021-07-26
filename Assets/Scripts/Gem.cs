using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Gem : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float liftTime;
    [SerializeField] private float rotationSpeed;

    [Header("Audio")]
    [SerializeField] private AudioClip collectedAudioClip;
    [SerializeField] private AudioSource audioSource;

    private Vector3 startPosition;


    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMove(endPosition, liftTime));
        sequence.Append(transform.DOLocalMove(startPosition, liftTime));
        sequence.SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            CollectGem();
        }
    }

    private void Update()
    {
        Rotate();
    }

    private void CollectGem()
    {
        audioSource.PlayOneShot(collectedAudioClip);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(OnCollect());
    }

    private void Rotate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0 );
    }

    private IEnumerator OnCollect()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
