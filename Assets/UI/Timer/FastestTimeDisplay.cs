using UnityEngine;
using TMPro;

public class FastestTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        text_box.text = GameData.FastestTime.ToString("0.00");
        
    }
}
