using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public bool doorOpen;

    private AudioSource _source;
    private SpriteRenderer _renderer;
    private Color32 _color;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>(); 
        _color = _renderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorOpen = true;
        _source.Play();
        StartCoroutine(Flicker());
        Destroy(GetComponent<BoxCollider2D>());
    }

    IEnumerator Flicker()
    {
        _renderer.color = Color.black;
        yield return new WaitForSeconds(.03f);
        _renderer.color = _color;
        yield return new WaitForSeconds(.03f);
        _renderer.color = Color.black;
        yield return new WaitForSeconds(.02f);
        _renderer.color = _color;
        yield return new WaitForSeconds(.02f);
        _renderer.color = Color.black;
        yield return new WaitForSeconds(.01f);
        _renderer.color = _color;
        yield return new WaitForSeconds(.01f);
        _renderer.color = Color.black;
        yield return new WaitForSeconds(.01f);
        _renderer.color = _color;
        yield return new WaitForSeconds(.01f);
        _renderer.color = Color.black;
        _renderer.enabled = false;
        yield return new WaitForSeconds(.15f);
        Destroy(this.gameObject);
    }

}
