using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float timer = 0.0f;
    float oldTime;
    float savedTime;

    public TextMeshProUGUI text_box;
    GameManager gManager;

    private void Start()
    {
        gManager = FindObjectOfType<GameManager>();
        oldTime = GameData.TotalTime;
    }

    void Update()
    {

        if (!gManager.stageCleared) timer += Time.deltaTime;
        else GameData.TotalTime = oldTime + timer;

        if (gManager.currentScene == "01-Start") GameData.TotalTime = 0f;
        if (gManager.currentScene == "04-End")
        {
            timer = 0f;
            if (GameData.FastestTime == 0) GameData.FastestTime = GameData.TotalTime;
            if (GameData.FastestTime >= GameData.TotalTime) 
                GameData.FastestTime = GameData.TotalTime;
        } 

        text_box.text = timer.ToString("0.00");


    }
}
