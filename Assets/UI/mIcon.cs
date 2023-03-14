using UnityEngine;
using TMPro;

public class mIcon : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        if (GameData.HasMover) text_box.text = "<sprite=0>";
        else if (!GameData.HasMover) text_box.text = "";
    }
}
