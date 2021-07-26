using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [SerializeField] private float timeDelay;
    [SerializeField] private float reloadTime;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float liftTime;
    [SerializeField] private float vibrationStrength;
    [SerializeField] private int vibrationRandomness;

    private Vector3 startPosition;
    private bool isTaken;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            isTaken = true;
            //StartCoroutine(OnPlatformLeave());
            DropPlatform();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            isTaken = false;
        }
    }

    private void DropPlatform()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(timeDelay);
        transform.DOShakePosition(timeDelay, vibrationStrength, vibrationRandomness);

        sequence.Append(transform.DOLocalMove(endPosition, liftTime));
        sequence.AppendInterval(reloadTime);
        sequence.Append(transform.DOLocalMove(startPosition, liftTime));

    }

    private IEnumerator OnPlatformLeave()
    {
        yield return new WaitForSeconds(timeDelay);
        if (isTaken)
        {
            var player =FindObjectOfType<Player>();
            player.SetPlayerDead();
        }
    }


}
