using UnityEngine;
using TMPro;

public class TotalTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        text_box.text = GameData.TotalTime.ToString("0.00");
    }


}
