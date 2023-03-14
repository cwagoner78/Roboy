using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public bool triggeredByPlayer;
    public bool triggeredByPrize;

    public float pistonForce = 63;

    PlayerController player;
    prizeCollected prize;

    AudioSource source;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        prize = FindObjectOfType<prizeCollected>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") triggeredByPlayer = true;
        if (other.gameObject.tag == "Prize") triggeredByPrize = true;

        if (triggeredByPlayer)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            player.rb.velocity = new Vector2(player.rb.velocity.x, pistonForce);
            source.pitch = 1;
            source.Play();
            StartCoroutine(Timer());
        }

        if (triggeredByPrize)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            prize.rb.velocity = new Vector2(prize.rb.velocity.x, pistonForce);
            source.pitch = 1.5f;
            source.Play();
            StartCoroutine(Timer());
        }

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.25f);
        if (transform.position.y != -1) transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        triggeredByPlayer = false;
        triggeredByPrize = false;
    }








}
