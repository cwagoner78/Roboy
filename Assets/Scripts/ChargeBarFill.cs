using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarFill : MonoBehaviour
{
    PlayerController player;
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.HasBattery)GetComponent<Image>().color = new Color32(125, 237, 249, 255);
        else GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
