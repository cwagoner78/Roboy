using UnityEngine;
using TMPro;

public class rIcon : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        if (GameData.HasRocket) text_box.text = "<sprite=0>";
        else if (!GameData.HasRocket) text_box.text = "";
    }
}
