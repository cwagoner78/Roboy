using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public DoorButton _doorButton;
    AudioSource _source;
    SpriteRenderer _renderer;

    private void Start()
    {
        _source = GetComponent<AudioSource>();   
        _renderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (_doorButton.doorOpen)
        {
            _source.pitch -= 0.01f;
            StartCoroutine(Flicker());
        }

        IEnumerator Flicker()
        {
            yield return new WaitForSeconds(.35f);
            _renderer.enabled = false;
            yield return new WaitForSeconds(.03f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(.03f);
            _renderer.enabled = false;
            yield return new WaitForSeconds(.02f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(.02f);
            _renderer.enabled = false;
            yield return new WaitForSeconds(.01f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(.01f);
            _renderer.enabled = false;

            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
