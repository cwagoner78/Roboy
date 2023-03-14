using System.Collections;
using UnityEngine;
using UnityEngine.Audio;



public class PlayerController : MonoBehaviour
{
    [Header("Collections")]
    public AudioClip[] audioClips;

    [Header("Input/Player Physics")]
    public bool inputEnabled;
    public float inputBuffer = 0.5f;
    public float moveSpeed = 10;
    public float moveSpeedMultiplier = 1.75f;
    public float jumpForce = 15;
    public float maxJumpForce = 20;
    public int jumpCost = 100;
    public float secondsBeforeReset = 1.5f;
    public int knockback = 10;
    public float wMomentum = 1.2f;

    [Header("AbilityFlags")]
    public bool canSetSpawn;
    public bool usingCharge;
    public bool playerMoving;
    public bool playerDead;
    public bool setSpawn;
    public bool playerSpawned;
    public bool playerGrounded;
    public bool playerOnWall;
    public bool isRunning;
    public bool hitPiston;
    public bool isDying;
    public bool isAttacking;
    public int health;

    [Header("Debugging Flags")]
    public bool debugEnabled;
    public bool infiniteScrap;
    public bool hasMover;
    public bool hasGrappler;
    public bool hasBattery;
    public bool hasRocket;
    public bool hasArmor;
    public bool killPlayer;

    [HideInInspector]
    public Vector2 currentMove;
    [HideInInspector]
    public Vector2 lastMove;
    [HideInInspector]
    public Vector2 spawnPoint;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer rend;
    [HideInInspector]
    public ParticleSystem pPart;

    float startSpeed;
    float maxSpeed;
    float startingJumpForce;
    int startingJumpCost;
    bool canJump;
    private string currentState;

    CameraController cameraController;
    ItemManager itemManager;
    BasicItemOverCharge overCharge;
    AudioSource source;
    SpawnPoint spawn;
    NoSpawnSetZone noSpawnZone;
    TrailRenderer trail;
    Animator anim;
    Quaternion startingRotation;


    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        pPart = GetComponent<ParticleSystem>();
        cameraController = FindObjectOfType<CameraController>();
        spawn = FindObjectOfType<SpawnPoint>();
        itemManager = FindObjectOfType<ItemManager>();
        overCharge = FindObjectOfType<BasicItemOverCharge>();
        noSpawnZone = FindObjectOfType<NoSpawnSetZone>();
        trail = GetComponent<TrailRenderer>();

        spawnPoint = spawn.transform.position;
        startSpeed = moveSpeed;
        maxSpeed = startSpeed * moveSpeedMultiplier;
        startingRotation = transform.rotation;
        startingJumpCost = jumpCost;
        startingJumpForce = jumpForce;
        canSetSpawn = true;

        ResetPlayer();
 
    }

    void Update()
    {
        DebugConsole();

        health = FindObjectOfType<PlayerHealth>().health;
        CheckOverCharge();
        PlayerInput();
        UpdateMovementStatus();
        CheckIfPlayerDead();

    }

    private void DebugConsole()
    {
        if (debugEnabled)
        {
            if (hasMover) GameData.HasMover = true;
            else if (!hasMover) GameData.HasMover = false;
            if (hasGrappler) GameData.HasGrappler = true;
            else if (!hasGrappler) GameData.HasGrappler = false;
            if (hasRocket) GameData.HasRocket = true;
            else if (!hasRocket) GameData.HasRocket = false;
            if (hasBattery) GameData.HasBattery = true;
            else if (!hasBattery) GameData.HasBattery = false;
            if (killPlayer) StartCoroutine(Die());
  
            if (infiniteScrap)
            {
                GameData.TotalScrap = 55;
                infiniteScrap = false;
            }
        }
    }

    public void PlayerInput()
    {
        var inputHorizontal = Input.GetAxis("Horizontal");
        var inputJump = Input.GetButtonDown("South Button");
        var inputLButton = Input.GetButtonDown("Left Bumper");
        var inputRButton = Input.GetButtonDown("Right Bumper");
        var inputLTrigger = Input.GetAxisRaw("Left Trigger") > 0;

        if (inputEnabled && inputHorizontal > inputBuffer || inputEnabled && inputHorizontal < inputBuffer)
        {
            rb.velocity = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);
            currentMove = new Vector2(inputHorizontal, 0f);

            if (!inputLTrigger)
            {
                moveSpeed = startSpeed;
                if (trail.time > 0) trail.time -= .01f;
                isRunning = false;

            }
            else if (GameData.HasMover && inputLTrigger)
            {
                moveSpeed = moveSpeed * moveSpeedMultiplier;
                if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                if (trail.time < 0.3f) trail.time += .01f;
                isRunning = true;
            }
        }

        // Rules for jumping
        if (playerGrounded || GameData.HasGrappler && playerOnWall) canJump = true;
        else canJump = false;

        if (inputEnabled && canJump && inputJump)
        {
            if (GameData.HasBattery) jumpForce = maxJumpForce;
            if (!itemManager.hasCharge) return;
            else rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            itemManager.currentCharge -= jumpCost;
            source.PlayOneShot(audioClips[1]);
        }

        //Spawnpoint manipulation
        if (inputEnabled && canSetSpawn && inputLButton)
        {
            setSpawn = true;
            if (noSpawnZone != null) noSpawnZone.playerSetSpawn = true;
            spawnPoint = rb.position;
            pPart.Play();
        }

        if (noSpawnZone != null && Input.GetButtonUp("Select")) noSpawnZone.playerSetSpawn = false;

        if (inputEnabled && inputRButton)
        {
            playerSpawned = true;
            itemManager.currentCharge = itemManager.maxCharge;
            ResetPlayer();
            pPart.Play();
        }
    }

    void UpdateMovementStatus()
    {
        if (rb.velocity.x > 0 || rb.velocity.x < 0 || rb.velocity.y > 0 || rb.velocity.y < 0) playerMoving = true;
        else playerMoving = false;
    }

    public void CheckOverCharge()
    {
        if (overCharge == null) return;
        if (!GameData.IsOverCharged)
        {
            if (!playerDead)
            {
                rend.color = Color.white;
                trail.startColor = Color.white;
            } 
            
            jumpCost = startingJumpCost;
            jumpForce = startingJumpForce;
        }
        else
        {
            rend.color = Color.red;
            trail.startColor = Color.red;
            jumpCost = 0;
            jumpForce = jumpForce * 1.25f;
            if (jumpForce > maxJumpForce) jumpForce = maxJumpForce;
            itemManager.currentCharge = itemManager.maxCharge;
        }
    }

    public void ResetPlayer(){
        Debug.Log("Resetting player to: " + spawnPoint);
        trail.enabled = false;
        transform.position = spawnPoint;
        ChangeAnimationState("PlayerIdle");
        pPart.Stop();
        isDying = false;
        playerDead = false;
        inputEnabled = true;
        rend.enabled = true;
        trail.enabled = true;
        rend.color = Color.white;
        cameraController.cameraHeight = -10;
        transform.rotation = startingRotation;
        rb.freezeRotation = true;
        itemManager.currentCharge = itemManager.maxCharge;
        if (GameData.HasArmor) GameData.Health = 2;
        else GameData.Health = 1;
    }

    public void CheckIfPlayerDead()
    {
        if (overCharge == null) return;
        if (GameData.IsOverCharged) return;
        if (!playerDead) return;
        if (!isDying) StartCoroutine(Die());
    }
    
    public IEnumerator Die()
    {
        killPlayer = false;
        isDying = true;
        GameData.TotalDeaths += 1;
        ChangeAnimationState("PlayerDie");
        source.PlayOneShot(audioClips[2]);
        inputEnabled = false;
        rb.freezeRotation = false;
        trail.enabled = false;
        pPart.Play();
        yield return new WaitForSeconds(secondsBeforeReset);
        isDying = false;
        ResetPlayer();
    }

    public void PlayPickUpSound() {
        source.PlayOneShot(audioClips[3]);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameData.IsOverCharged) return;
        if (other.gameObject.tag != "GivesDamage") return;
        if (GameData.HasArmor)
        {
            Vector2 direction = (this.transform.position - other.transform.position).normalized;
            rb.AddForce(direction * knockback);
        }

    }

}