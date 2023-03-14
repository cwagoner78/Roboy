using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpawnSetZone : MonoBehaviour
{
    public bool isTriggered;
    public bool playerSetSpawn;

    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTriggered = true;
            player.canSetSpawn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTriggered = false;
            player.canSetSpawn = true;
        }
    }
}
