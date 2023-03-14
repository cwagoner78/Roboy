using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool isTouched;

    public PlayerController player;
    AudioSource source;

    private void Start(){
        isTouched = false;
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject != player.gameObject) return;
        isTouched = true;
        player.gameObject.SetActive(false);
        source.Play();
    }

}
