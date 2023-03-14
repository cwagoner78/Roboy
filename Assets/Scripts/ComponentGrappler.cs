using System.Collections;
using UnityEngine;


public class ComponentGrappler : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public bool isFound;

    PlayerController player;
    AudioSource source;
    SpriteRenderer rend;
    BoxCollider2D boxCollider;
    ItemManager itemManager;
    ParticleSystem pSystem;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();



    void Start()
    {
        if (GameData.HasGrappler) Destroy(this.gameObject);
        isFound = false;
        posOffset = transform.position;
        player = FindObjectOfType<PlayerController>();
        pSystem = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        itemManager = FindObjectOfType<ItemManager>();
        boxCollider.enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.PlayPickUpSound();
            pSystem.Play();
            rend.enabled = false;
            isFound = true;
            GameData.HasGrappler = true;
            boxCollider.enabled = false;
            StartCoroutine(Destroy());
        }
    }

    void Update()
    {
        Float();
    }

    void Float()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
