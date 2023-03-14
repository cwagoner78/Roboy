using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCanvas : MonoBehaviour
{
    PlayerController player;
    Animator anim;

    [SerializeField]
    float offset;

    bool hasGrappler;
    bool hasMover;
    bool hasRocket;
    bool hasBattery;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x + offset, player.transform.position.y + offset);
    }
}



