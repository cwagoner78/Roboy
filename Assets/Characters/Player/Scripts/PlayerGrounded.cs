using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayer;

    PlayerController player;
    Vector2 playerPos;
    [SerializeField]
    float groundedRayOffset = 0.35f;
    [SerializeField]
    float groundedRayLength = 1.5f;

    [SerializeField]
    float wallCheckDistance = 0.5f;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        CheckGrounded();
        CheckOnWall();
    }

    public void CheckGrounded()
    {
        playerPos = player.transform.position;
        float bPosX = playerPos.x;
        float fPosX = playerPos.x;

        if (player.lastMove.x < 0)
        {
            bPosX = bPosX + groundedRayOffset;
            fPosX = fPosX - groundedRayOffset;
        }
        else
        {
            bPosX = bPosX - groundedRayOffset;
            fPosX = fPosX + groundedRayOffset;
        } 

        Vector2 bPos = new Vector2(bPosX, transform.position.y);
        Vector2 fPos = new Vector2(fPosX, transform.position.y);
        Vector2 cPos = new Vector2(playerPos.x, playerPos.y);

        Vector2 direction = Vector2.down;
        float distance = groundedRayLength;

        //Check Back
        Debug.DrawLine(bPos, new Vector2(bPosX , bPos.y - distance));
        RaycastHit2D hitB = Physics2D.Raycast(bPos, direction, distance, groundLayer);

        //Check Center
        Debug.DrawLine(cPos, new Vector2(cPos.x, cPos.y - distance));
        RaycastHit2D hitC = Physics2D.Raycast(cPos, direction, distance, groundLayer);

        //Check Front
        Debug.DrawLine(fPos, new Vector2(fPosX, fPos.y - distance));
        RaycastHit2D hitF = Physics2D.Raycast(fPos, direction, distance, groundLayer);

        if (hitF.collider != null || hitC.collider != null || hitB.collider != null) player.playerGrounded = true;
        else player.playerGrounded = false;
    }

    public void CheckOnWall()
    {
        Vector2 position = transform.position;
        Vector2 directionR = Vector2.right;
        Vector2 directionL = Vector2.left;
        float distance = wallCheckDistance;

        //Check Right
        RaycastHit2D hitR = Physics2D.Raycast(position, directionR, distance, groundLayer);
        Debug.DrawLine(position, new Vector2(position.x + distance, position.y));
        //Check Right
        RaycastHit2D hitL = Physics2D.Raycast(position, directionL, distance, groundLayer);
        Debug.DrawLine(position, new Vector2(position.x - distance, position.y));

        if (hitR.collider != null || hitL.collider != null) player.playerOnWall = true;
        else player.playerOnWall = false;
    }

}
