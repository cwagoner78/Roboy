using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{

    public Image fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        fadeOut.canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    public void FadingOut()
    {
        fadeOut.CrossFadeAlpha(1, 1, false);
    }
}