using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOut : MonoBehaviour
{
    private TMP_Text tMesh;
    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        tMesh = GetComponent<TMP_Text>();
        alpha = tMesh.alpha;
    }

    // Update is called once per frame
    public void Disappear()
    {
        alpha = 0;
    }
}
