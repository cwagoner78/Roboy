using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float intensityMult = .2f;
    public bool lockDirection = false;
    public bool facingRight;

    Animator anim;
    PlayerController player;
    SpriteRenderer rend;
    Rigidbody2D rb;

    private string currentState;

    void Start()
    {
        player = GetComponent<PlayerController>();
        anim = player.GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDirection();
        CheckAnimation();
    }

    private void CheckDirection()
    {
        if (lockDirection) return;

        var velY = rb.velocity.y;

        if (player.playerMoving && player.currentMove != new Vector2(0, velY))
            player.lastMove = player.currentMove;

        if (player.lastMove.x < 0)
        {
            rend.flipX = true;
            facingRight = false;
        }
        else
        {
            rend.flipX = false;
            facingRight = true;
        } 
    }

    private void CheckAnimation()
    {
        if (!player.playerGrounded || !player.playerOnWall)
        {
            if (!player.isDying && !player.isAttacking && player.rb.velocity.y > 0) ChangeAnimationState("PlayerFlying");
            if (!player.isDying && !player.isAttacking && player.rb.velocity.y < 0) ChangeAnimationState("PlayerFalling");
        }

        if (!player.isDying && !player.isAttacking && !player.isRunning && rb.velocity.y == 0) ChangeAnimationState("PlayerIdle");
        if (!player.isDying && !player.isAttacking && player.rb.velocity.y == 0 && player.isRunning) ChangeAnimationState("PlayerSprinting");
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }
}
