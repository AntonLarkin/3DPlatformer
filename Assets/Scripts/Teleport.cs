using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Vector3 teleportPosition;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            Debug.Log(teleportPosition);
            player.TeleportPlayer(teleportPosition);
        }
    }

}
