using System.Collections;
using UnityEngine;


public class ComponentRocket : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public bool isFound;

    PlayerController player;
    AudioSource source;
    SpriteRenderer rend;
    ItemManager itemManager;
    BoxCollider2D _collider;
    ParticleSystem pSystem;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();


    void Start()
    {
        if (GameData.HasRocket) Destroy(this.gameObject);
        isFound = false;
        posOffset = transform.position;
        player = FindObjectOfType<PlayerController>();
        pSystem = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        itemManager = FindObjectOfType<ItemManager>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.PlayPickUpSound();
            pSystem.Play();
            rend.enabled = false;
            GameData.HasRocket = true;
            _collider.enabled = false;
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
