using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prizeCollected : MonoBehaviour
{
    public bool isRescued;
    public float maxSizeOnPrize;
    private Animator anim;
    private new Transform transform;
    public Rigidbody2D rb;


    GameManager gameManager;

    public AudioClip[] clips;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        isRescued = false;
        anim.enabled= false;    
   
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var clip = clips[Random.Range(0, clips.Length)];
            source.pitch = Random.Range(1.2f,1.7f);
            source.PlayOneShot(clip);

            isRescued = true;
            anim.enabled = true;

            gameManager.stageCleared = true;

            if (transform.localScale.x < maxSizeOnPrize || transform.localScale.y < maxSizeOnPrize)
                transform.localScale = transform.localScale * 1.1f;
        }


    }
}
