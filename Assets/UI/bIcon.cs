using UnityEngine;
using TMPro;

public class bIcon : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        if (GameData.HasBattery) text_box.text = "<sprite=0>";
        else if (!GameData.HasBattery) text_box.text = "";
    }
}
