using UnityEngine.UI;
using UnityEngine;

public class ChargeBar1 : MonoBehaviour
{

    public Slider slider;
    ItemManager itemManager;
    private float currentCharge;
    private float maxCharge;

    private void Awake()
    {
        itemManager = FindObjectOfType<ItemManager>();
        slider = GetComponent<Slider>();

        SetMaxCharge();
        
        maxCharge = itemManager.maxCharge;
        slider.maxValue = maxCharge;
    }

    private void Update()
    {
        SetCharge();
    }

    public void SetMaxCharge()
    {
        slider.maxValue = maxCharge;
        slider.value = currentCharge;
    }

    public void SetCharge()
    {
        currentCharge = itemManager.currentCharge;
        slider.value = currentCharge;
    }

}
