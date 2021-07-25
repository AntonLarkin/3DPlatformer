using UnityEngine;
using DG.Tweening;

public class Gem : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float liftTime;
    [SerializeField] private float rotationSpeed;

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
        Destroy(gameObject);
    }

    private void Rotate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0 );
    }



}
