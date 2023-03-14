using UnityEngine;
using TMPro;

public class TotalScrapCounter : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        text_box.text = GameData.ScrapCollected.ToString("0");

    }
}