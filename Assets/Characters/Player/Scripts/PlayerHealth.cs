using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool playerHit;
    HurtPlayer hurtPlayer;
    PlayerController player;
    SpriteRenderer armorRend;
    AudioSource source;

    public AudioClip[] clips;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        hurtPlayer = FindObjectOfType<HurtPlayer>();
        armorRend = FindObjectOfType<PlayerArmor>().GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();

        if (GameData.HasArmor)
        {
            GameData.Health = 2;
            armorRend.enabled = true;
        } 
        else GameData.Health = 1;
    }

    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        health = GameData.Health;

        if (GameData.IsOverCharged) return;

        if (playerHit)
        {
            Debug.Log("Player hit");
            GameData.Health -= hurtPlayer.damage;
            GameData.IsInvincible = true;
            var clip = clips[Random.Range(0, clips.Length)];
            if (!player.playerDead) source.PlayOneShot(clip);

            playerHit = false;
        } 

        if (GameData.Health == 1)
        {
            GameData.HasArmor = false;
            armorRend.enabled = false;
        }

        if (GameData.Health <= 0) player.playerDead = true;

    }



}
