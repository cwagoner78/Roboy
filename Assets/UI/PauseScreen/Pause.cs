using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{

    GameManager gManager;
    Canvas canvas;
    public AudioSource source;

    public bool canPause;

    void Start()
    {
        source = GetComponent<AudioSource>();
        gManager = FindObjectOfType<GameManager>();
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (!canPause) return;
        else if (gManager.gamePaused) canvas.enabled = true;
        else canvas.enabled = false;

    }
}
