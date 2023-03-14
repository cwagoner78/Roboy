using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public AudioClip[] clips;

    PlayerController player;
    ParticleSystem.EmissionModule particles;
    public ParticleSystem _particleSystem;
    AudioSource source;
    prizeCollected prize;

    public Vector2 spawnPointLocation;
    public Vector2 spawnPointStartLocation;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();
        _particleSystem = GetComponent<ParticleSystem>();
        prize = FindObjectOfType<prizeCollected>();
        spawnPointStartLocation = transform.position;
    }

    private void Update()
    {
        //if (!prize.isRescued)
        //{
        //    if (player.setSpawn) source.PlayOneShot(clips[0]);
        //    if (player.playerSpawned) source.PlayOneShot(clips[1]);

        //    transform.position = new Vector2(player.spawnPoint.x, player.spawnPoint.y);
        //    spawnPointLocation = transform.position;

        //    player.setSpawn = false;
        //    player.playerSpawned = false;
        //}

        if (player.setSpawn) source.PlayOneShot(clips[0]);
        if (player.playerSpawned) source.PlayOneShot(clips[1]);

        transform.position = new Vector2(player.spawnPoint.x, player.spawnPoint.y);
        spawnPointLocation = transform.position;

        player.setSpawn = false;
        player.playerSpawned = false;

    }


}
