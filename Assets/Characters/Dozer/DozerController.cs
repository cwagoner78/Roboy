using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DozerController : MonoBehaviour
{
    public float speed;
    public bool stop;
    public bool triggered;

    AudioSource source;

    //Animation
    Animator anim;
    const string DOZER_IDLE = "DozerNew-Idle";
    const string DOZER_MOVING = "DozerNew-Drive";
    private string currentAnimation;
    private string currentState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        MoveDozer();

        //Change Dozer's layer so player can jump off of him if they have grappler
        if (GameData.HasGrappler) gameObject.layer = 8; 
    }

    private void MoveDozer()
    {
        if (!triggered) return;
        if (stop)
        {
            transform.position = transform.position;
            source.Stop();
            ChangeAnimationState(DOZER_IDLE);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
            if (!source.isPlaying)source.Play();
            ChangeAnimationState(DOZER_MOVING);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "ZoneTrigger") return;
        stop = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") triggered = true;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}

