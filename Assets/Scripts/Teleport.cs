using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport destinationTeleport;

    private Vector3 teleportPosition;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            player.TeleportPlayer(SetTeleportPosition());
            destinationTeleport.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(OnEnter());
            
        }
    }

    private Vector3 SetTeleportPosition()
    {
        return teleportPosition = destinationTeleport.transform.position;
    }

    private IEnumerator OnEnter()
    {
        yield return new WaitForSeconds(3f);
        destinationTeleport.GetComponent<BoxCollider>().enabled = true;
    }
}
