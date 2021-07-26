using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ButtonToMoveObjects : MonoBehaviour
{
    [SerializeField] private MovableObject obstacle;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float liftTime;
    [SerializeField] private Vector3 startPosition;

    [Header("Audio")]
    [SerializeField] private AudioClip activateButtonAudioClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        MovePressedButton(endPosition);
        audioSource.PlayOneShot(activateButtonAudioClip);
        obstacle.LiftBox(obstacle.EndPosition);
    }

    private void OnTriggerExit(Collider other)
    {
        MovePressedButton(startPosition);
        audioSource.PlayOneShot(activateButtonAudioClip);
        if (obstacle.IsNeedToBeReturned)
        {
            StartCoroutine(OnLeavingButton());
        }
    }

    private void MovePressedButton(Vector3 destinationPosition)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(destinationPosition, liftTime));
    }

    private IEnumerator OnLeavingButton()
    {
        yield return new WaitForSeconds(7f);

        obstacle.LiftBox(obstacle.StartPosition);
    }
}
