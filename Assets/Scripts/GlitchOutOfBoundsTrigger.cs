using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchOutOfBoundsTrigger : MonoBehaviour
{

    GameManager gManager;

    private void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gManager.Reload();
    }
}
