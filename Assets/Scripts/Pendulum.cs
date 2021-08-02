using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endposition;
    [SerializeField] private float timeDelay;
    [SerializeField] private float movementDuration;
    [SerializeField] private AnimationCurve animationCurve;

    private void Start()
    {
        StartAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            other.GetComponent<Player>().SetPlayerDead();
        }
    }
    private void StartAnimation()
    {
        Sequence launchS = DOTween.Sequence();
        launchS.Append(transform.DOLocalRotate(endposition, movementDuration).SetEase(animationCurve));
        launchS.AppendInterval(timeDelay);
        launchS.Append(transform.DOLocalRotate(startPosition, movementDuration).SetEase(animationCurve));
        launchS.AppendInterval(timeDelay);
        launchS.SetLoops(-1);
    }

}
