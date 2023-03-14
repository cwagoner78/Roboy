using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextGotoNextLevel : MonoBehaviour
{
    private bool prize;
    private TMP_Text tMesh;
    private GameManager manager;

    private float alpha;
    private Color32 color;

    void Start()
    {
        tMesh = GetComponent<TMP_Text>();
        manager = FindObjectOfType<GameManager>();
        alpha = tMesh.alpha;
        color = tMesh.color;

    }

    void Update()
    {
        prize = FindObjectOfType<prizeCollected>().isRescued;

        if (!prize) tMesh.alpha = 0;
        else tMesh.color = Color.white;

        if (manager.startPressed) tMesh.color = new Color32(98, 109, 161, 255);

    }
}
