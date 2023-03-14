using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;

public class BasicItemOverCharge : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float itemRespawnTime = 6;

    PlayerController player;
    AudioSource source;
    SpriteRenderer rend;
    ItemManager itemManager;
    ParticleSystem pSystem;
    CircleCollider2D circ_Collider;
    GameManager gameManager;

    Light2D ocLight;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();


    void Start(){

        ocLight = GetComponentInChildren<Light2D>();
        posOffset = transform.position;
        player = FindObjectOfType<PlayerController>();
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        itemManager = FindObjectOfType<ItemManager>();
        pSystem = GetComponent<ParticleSystem>();
        circ_Collider = GetComponent<CircleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();

        circ_Collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player")
        {
            GameData.IsOverCharged = true;
            player.PlayPickUpSound();
            pSystem.Play();
            rend.enabled = false;
            ocLight.enabled = false;
            circ_Collider.enabled = false;
            StartCoroutine(Timer());
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        source.mute = true;
    }

    void Update(){
        Float();
        CheckIsFound();

    }

    void CheckIsFound(){
        if (GameData.IsOverCharged)
            source.volume = .3f;
        else
            source.volume = 0;
    }

    void Float(){
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(itemRespawnTime);
        GameData.IsOverCharged = false;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(itemRespawnTime);
        rend.enabled = true;
        ocLight.enabled = true;
        circ_Collider.enabled = true;
    }

}

