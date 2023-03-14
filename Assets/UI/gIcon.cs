using UnityEngine;
using TMPro;

public class gIcon : MonoBehaviour
{
    public TextMeshProUGUI text_box;

    void Update()
    {
        if (GameData.HasGrappler) text_box.text = "<sprite=0>";
        else if (!GameData.HasGrappler) text_box.text = "";
    }
}
