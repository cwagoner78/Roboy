
using UnityEngine;
using TMPro;

public class ScrapCounter : MonoBehaviour
{

    public TextMeshProUGUI text_box;
    int totalScrap;

    void Update()
    {
        totalScrap = GameData.TotalScrap;

        text_box.text = "<sprite=0> " + totalScrap.ToString("0");
                       
    }
}
