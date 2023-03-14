using UnityEngine;

public class PlayerRocket : MonoBehaviour
{

    public SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!GameData.HasRocket) rend.enabled = false;
        else rend.enabled = true;
    }
}
