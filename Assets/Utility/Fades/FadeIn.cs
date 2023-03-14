using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadeIn;

    // Start is called before the first frame update
    void Start()
    {
        fadeIn.canvasRenderer.SetAlpha(1);

    }

    // Update is called once per frame
    public void FadingIn ()
    {
        fadeIn.CrossFadeAlpha(0,1,false);   
    }
}
