using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer rend;
    
    PlayerController player;

    public int armorValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.lastMove.x < 0) rend.flipX = true;
        else rend.flipX = false;
    }
}
