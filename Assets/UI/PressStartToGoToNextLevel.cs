using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressStartToGoToNextLevel : MonoBehaviour
{

    private bool prize;
    private bool startPressed;
    private TMP_Text tMesh;
    private GameManager manager;
    private Material material;

    private float alpha;
    private Color32 color;
    
    // Start is called before the first frame update
    void Start()
    {
        tMesh = GetComponent<TMP_Text>();
        manager = FindObjectOfType<GameManager>();
        alpha = tMesh.alpha;
        color = tMesh.color;

    }

    // Update is called once per frame
    void Update()
    {
        
        prize = FindObjectOfType<prizeCollected>().isRescued;
        if (!prize)
            
        {
            tMesh.alpha = 0;
        }
        else
            tMesh.color = Color.white;

        if (manager.startPressed)

        {
            tMesh.color = new Color32(98,109,161,255);
            
           
        }

    }
}
