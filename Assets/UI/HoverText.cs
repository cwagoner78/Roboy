using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverText : MonoBehaviour
{
    TextMeshProUGUI tmp;
    TMP_Text text;
    public TextTrigger textTrigger;
    PlayerController player;
    public bool triggered;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        text = GetComponent<TMP_Text>();
        player = FindObjectOfType<PlayerController>();
        if (textTrigger == null) Destroy(this.gameObject);
    }

    void Update()
    {
        if (textTrigger == null) return;
        if (textTrigger.triggered)
        {
            var rate = (0.0000001f * Time.deltaTime);
            tmp.enabled = true;
            text.alpha -= rate;
            StartCoroutine(KillTextBox());
        }
        else tmp.enabled = false;
    }

    IEnumerator KillTextBox()
    {
        Time.timeScale = 0;
        player.inputEnabled = false;
        yield return new WaitForSecondsRealtime(1);
        if (Input.anyKeyDown)
        {
            player.inputEnabled = true;
            Time.timeScale = 1;
            Destroy(textTrigger);
            Destroy(this.gameObject);
        }
        yield return new WaitForSecondsRealtime(99999);
        player.inputEnabled = true;
        Time.timeScale = 1;
        Destroy(textTrigger);
        Destroy(this.gameObject);
    }
}
