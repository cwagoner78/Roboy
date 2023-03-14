using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattery : MonoBehaviour
{
    PlayerController player;
    public SpriteRenderer bRend;

    bool hasBattery;
   
    public Vector2 lastMove;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        bRend = GetComponent<SpriteRenderer>();

        hasBattery = false;
    }

    void Update()
    {
        hasBattery = GameData.HasBattery;
        if (!hasBattery) bRend.enabled = false;
        else bRend.enabled = true;

        lastMove = player.lastMove;
        if (lastMove.x < 0) bRend.flipX = true;
        else bRend.flipX = false;
    }
}

