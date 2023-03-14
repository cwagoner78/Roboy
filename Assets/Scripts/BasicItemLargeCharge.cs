using System.Collections;
using UnityEngine;


public class BasicItemLargeCharge : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public bool isFound;
    public int chargeReplenish = 500;
    public float itemRespawnTime = 6;

    AudioSource source;
    SpriteRenderer rend;
    ItemManager itemManager;
    ParticleSystem pSystem;
    CircleCollider2D circ_Collider;
    PlayerController player;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();


    void Start()
    {
        isFound = false;
        posOffset = transform.position;
        player = FindObjectOfType<PlayerController>();
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        itemManager = FindObjectOfType<ItemManager>();
        pSystem = GetComponent<ParticleSystem>();
        circ_Collider = GetComponent<CircleCollider2D>();
        circ_Collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isFound = true;
            //degreesPerSecond = 0;
            source.Play();
            pSystem.Play();
            rend.enabled = false;
            StartCoroutine(Respawn());
            itemManager.currentCharge = itemManager.currentCharge + chargeReplenish;
            circ_Collider.enabled = false;
        }
    }

    void Update()
    {
        Float();
        if (isFound)
            source.volume = .3f;
        else
            source.volume = 0;
    }

    void Float()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(itemRespawnTime);
        isFound = false;
        yield return new WaitForSeconds(5);
        rend.enabled = true;
        circ_Collider.enabled = true;
    }

}
