using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmorBotText : MonoBehaviour
{
    public bool playerInteracted;


    TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playerInteracted)
        {
            tmp.enabled = true;
            StartCoroutine(KillTextBox());
        }
        else tmp.enabled = false;
    }

    IEnumerator KillTextBox()
    {
        yield return new WaitForSeconds(4);
        tmp.enabled = false;
        playerInteracted = false;
    }
}
