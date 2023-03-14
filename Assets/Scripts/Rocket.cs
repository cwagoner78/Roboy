using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;

    PlayerController player;
    ItemManager itemManager;
    Rigidbody2D rb;
    ParticleSystem rPart;
    PlayerRocket pRocket;
    AudioSource source;
    Animator anim;

    public string currentState;
    
    public float rocketForce;
    public float maxRocketForce;


    void Start()
    {
        player = GetComponent<PlayerController>();
        itemManager = GetComponent<ItemManager>();
        rb = GetComponent<Rigidbody2D>();
        pRocket = FindObjectOfType<PlayerRocket>();
        rPart = pRocket.GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.playerDead) return;

        //InputRocket
        var inputRTrigger = Input.GetAxisRaw("Right Trigger") > 0;

        if (GameData.HasBattery) rocketForce = maxRocketForce;

        if (GameData.HasRocket && itemManager.hasCharge && inputRTrigger)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + rocketForce);
            rPart.Play();
            source.PlayOneShot(clips[0]);
            if (!GameData.IsOverCharged)
                itemManager.usingCharge = true;
            else itemManager.usingCharge = false;

        }
        else if (!inputRTrigger || !itemManager.hasCharge)
        {
            rPart.Stop();
            itemManager.usingCharge = false;
        }
    }
}
