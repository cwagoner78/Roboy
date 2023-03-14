using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBot : MonoBehaviour
{
    PlayerArmor armor;
    AudioSource source;
    public AudioClip[] clips;
    public ArmorBotText abText;
    public bool canTalk = false;
    public bool isTalking;
    public int armorCost = 10;

    //Animation
    Animator anim;
    private string currentState;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        abText = FindObjectOfType<ArmorBotText>();
        armor = FindObjectOfType<PlayerArmor>();
        
        ChangeAnimationState("ArmorBot-Idle");
        if (GameData.HasArmor) armor.rend.enabled = true;
    }

    private void Update()
    {
        var inputInteract = Input.GetButtonDown("North Button");

        if (!canTalk || isTalking) return;
        if (inputInteract)
        {
            isTalking = true;
            var clip = clips[Random.Range(0,clips.Length)];
            abText.playerInteracted = true;
            source.PlayOneShot(clip);
            ChangeAnimationState("ArmorBot-EjectItem");
            StartCoroutine(Timer());

            if (GameData.HasArmor) return;
            if (GameData.TotalScrap >= armorCost)
            {
                GameData.HasArmor = true;
                GameData.Health += armor.armorValue;
                armor.rend.enabled = true;
                GameData.TotalScrap -= armorCost;
            }
            else if (!GameData.HasArmor) armor.rend.enabled = false;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(2);
        ChangeAnimationState("ArmorBot-Idle");
        isTalking = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") canTalk = true;
        else canTalk = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canTalk = false;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
