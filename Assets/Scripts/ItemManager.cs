using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ItemManager : MonoBehaviour
{

    public bool debugEnabled = false;

    //Component flags
    [Header("Public Variables")]
    bool playerHasRocket;
    bool playerHasMover;
    bool playerHasGrapple;
    bool playerHasBattery;


    public bool hasCharge;
    public bool usingCharge;
    public float startingCharge;

    public float currentCharge;
    public float maxCharge = 1000;
    public float chargeCost = 5;

    [Header("Controls")]
    [SerializeField]
    float rechargeRateMultiplier = 1;
    [SerializeField]
    float batteryRechargeRateMult = 1.25f;

    //collectibles
    ComponentRocket c_Rocket;
    ComponentMover c_Mover;
    ComponentGrappler c_Grappler;
    ComponentBattery c_Battery;
    BasicItemOverCharge overCharge;
    SpriteRenderer rend;

    public Sprite spriteNoHelmet;
    public Sprite spriteWithHelmet;

    void Awake()
    {
        playerHasGrapple = GameData.HasGrappler;
        playerHasMover = GameData.HasMover;
        playerHasRocket = GameData.HasRocket;
        playerHasBattery = GameData.HasBattery;
    }

    void Start()
    {
        c_Rocket = FindObjectOfType<ComponentRocket>();
        c_Grappler = FindObjectOfType<ComponentGrappler>();
        c_Mover = FindObjectOfType<ComponentMover>();
        c_Battery = FindObjectOfType<ComponentBattery>();
        overCharge = FindObjectOfType<BasicItemOverCharge>();
        rend = GetComponent<SpriteRenderer>();
        currentCharge = maxCharge;
        startingCharge = currentCharge;
    }


    private void FixedUpdate()
    {
        UpdateCharge();
        UpdateRocket();
        UpdateMover();
        UpdateGrapple();
        UpdateBattery();
        
    }

    public void UpdateCharge()
    {
        if (currentCharge >= 90)
            hasCharge = true;
        if (currentCharge <= 10)
            hasCharge = false;

        if (GameData.HasBattery)
        {
            rechargeRateMultiplier = batteryRechargeRateMult;
        }

        if (!usingCharge)
            currentCharge = currentCharge + rechargeRateMultiplier;
        else if (usingCharge)
            currentCharge -= chargeCost;

        //clamps
        if (currentCharge < 1) currentCharge = 1;
        if (currentCharge > maxCharge) currentCharge = maxCharge;
    }



    void UpdateRocket()
    {
        if (!GameData.HasRocket) return;
        else if (GameData.HasRocket)
        {
            playerHasRocket = true;
            Destroy(c_Rocket);
        }
    }

    void UpdateMover()
    {
        if (!GameData.HasMover)return;
        else if (GameData.HasMover){
            playerHasMover = true;
            Destroy(c_Mover);
        }
    }

    void UpdateGrapple()
    {
        if (!GameData.HasGrappler) return;
        else if (GameData.HasGrappler)
        {
            playerHasGrapple = true;
            Destroy(c_Grappler);
        }
    }
    void UpdateBattery()
    {
        if (!GameData.HasBattery) return;
        else if (GameData.HasBattery)
        {
            playerHasBattery = true;
            Destroy(c_Battery);
        }
    }
}

