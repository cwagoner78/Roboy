using System.Collections;
using UnityEngine;

public class ComponentBattery : MonoBehaviour
{
    [Header("Float Settings")]
    [SerializeField]
    float degreesPerSecond = 0f;
    [SerializeField]
    float amplitude = 0.5f;
    [SerializeField]
    float frequency = 1f;
    public bool isFound;

    PlayerController player;
    SpriteRenderer rend;
    BoxCollider2D _collider;
    ItemManager itemManager;
    ParticleSystem pSystem;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        if (GameData.HasBattery) Destroy(this.gameObject);
        isFound = false;
        posOffset = transform.position;
        player = FindObjectOfType<PlayerController>();
        pSystem = GetComponent<ParticleSystem>();
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
            GameData.HasBattery = true;
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
