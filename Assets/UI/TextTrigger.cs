using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public bool triggered;
    public GameObject player;
    AudioSource source;


    private void Start() { 
        source= GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) triggered = true;

        source.Play();
    }
}
