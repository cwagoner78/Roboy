using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Level Management")]
    public string previousScene;
    public string currentScene;
    public string nextScene;
    public string altNextScene;
    public string completionistScene;
    public bool startPressed = false;
    public bool selectPressed = false;
    public bool gamePaused = false;

    //Zone flags
    public bool prizeIsFound;
    public bool stageCleared;
    public bool gameCleared;
    public bool completionist = false;

    prizeCollected prize;
    PlayerController player;
    AudioSource source;
    BasicItemOverCharge overCharge;
    Portal portal;
    Pause pause;

    FadeOut fadeOut;
    FadeIn fadeIn;
    float startingVolume;
    float fadeOutTime = 2f;

    private void Start()
    {
        Time.timeScale = 1;

        player = FindObjectOfType<PlayerController>();
        prize = FindObjectOfType<prizeCollected>();
        source = GetComponent<AudioSource>();
        overCharge = FindObjectOfType<BasicItemOverCharge>();
        startingVolume = source.volume;
        fadeIn = FindObjectOfType<FadeIn>();
        fadeOut = FindObjectOfType<FadeOut>();
        fadeIn.FadingIn();
        portal = FindObjectOfType<Portal>();
        pause = FindObjectOfType<Pause>();
        GameData.IsOverCharged = false;
        stageCleared = false;
        if (currentScene == "04-End") gameCleared = true;
        if (currentScene == "01-Start")
        {
            GameData.TotalScrap = 0;
            GameData.ScrapCollected = 0;
            GameData.TotalDeaths = 0;
            GameData.Completionist = false;
        }


    }

    void Update()
    {
        var inputStart = Input.GetButtonDown("Start");

        //Debug
        var inputPreviousLevel = Input.GetKeyDown(KeyCode.PageUp);
        var inputRestartLevel = Input.GetKeyDown(KeyCode.Home);
        var inputNextLevel = Input.GetKeyDown(KeyCode.PageDown);
        var inputAltNextLevel = Input.GetKeyDown(KeyCode.End);
        var inputCompletionistLevel = Input.GetKeyDown(KeyCode.Insert);

        if (inputPreviousLevel) StartCoroutine(LoadPreviousScene());
        if (inputRestartLevel) StartCoroutine(LoadCurrentScene());
        if (inputNextLevel) StartCoroutine(LoadNextScene());
        if (inputAltNextLevel) StartCoroutine(LoadAltNextScene());
        if (inputCompletionistLevel) StartCoroutine(LoadAltNextScene());

        completionist = GameData.Completionist;

        if (prize.isRescued)
        {
            pause.canPause = false;
            if (GameData.Completionist)
            {
                if (inputStart)
                {
                    startPressed = true;
                    StartCoroutine(LoadCompletionistScene());
                    Debug.Log("Going to level " + completionistScene);
                }
            }
            else if (inputStart)
            {
                startPressed = true;
                StartCoroutine(LoadNextScene());
                Debug.Log("Going to level " + nextScene);
            }

            if (currentScene == completionistScene) GameData.Completionist = false;
            else 
            {
                if (GameData.ScrapCollected >= 55 &&
                    GameData.HasMover &&
                    GameData.HasBattery &&
                    GameData.HasGrappler &&
                    GameData.HasRocket) GameData.Completionist = true;
                else GameData.Completionist = false;
            }

        }
        else pause.canPause = true;

        if (portal != null)
            if (portal.isTouched) StartCoroutine(LoadAltNextScene());

        //maybe this should be in an audio manager
        if (overCharge == null) return;
        if (!GameData.IsOverCharged) source.pitch = 1;
        else if (GameData.IsOverCharged)source.pitch = 1.5f;

        CheckForPause();
    }

    void CheckForPause()
    {
        if (pause.canPause && Input.GetButtonDown("Start"))
        {
            if (!gamePaused)
            {
                gamePaused = true;
                Time.timeScale = 0f;
                player.inputEnabled = false;
                player.rb.simulated = false;
                source.Pause();
                pause.source.Play();
            }
            else
            {
                gamePaused = false;
                Time.timeScale = 1f;
                player.inputEnabled = true;
                player.rb.simulated = true;
                source.Play();
                pause.source.Play();
            }
        }

        if (gamePaused)
        {
            if (Input.GetButtonDown("Select")) StartCoroutine(LoadCurrentScene());
        }
    }

    IEnumerator LoadPreviousScene()
    {
        Time.timeScale = 1;
        stageCleared = true;
        fadeOut.FadingOut();
        source.volume -= Time.deltaTime / fadeOutTime;
        source.volume -= 0.0000001f;

        yield return new WaitForSecondsRealtime(fadeOutTime);
        GameData.CurrentScene = previousScene;
        SceneManager.LoadScene(previousScene);

        player.ResetPlayer();
        source.volume = 0;
        source.Play();
        source.volume += .0000001f;

        if (source.volume > startingVolume) source.volume = startingVolume;
        prize.isRescued = false;

    }

    public void Reload()
    {
        StartCoroutine(LoadCurrentScene());
    }

    IEnumerator LoadCurrentScene()
    {
        Time.timeScale = 1;
        stageCleared = true;
        fadeOut.FadingOut();
        source.volume -= Time.deltaTime / fadeOutTime;
        source.volume -= 0.0000001f;

        yield return new WaitForSecondsRealtime(fadeOutTime);
        GameData.CurrentScene = currentScene;
        SceneManager.LoadScene(currentScene);

        player.ResetPlayer();
        source.volume = 0;
        source.Play();
        source.volume += .0000001f;

        if (source.volume > startingVolume) source.volume = startingVolume;
        prize.isRescued = false;
        
    }

    IEnumerator LoadNextScene()
    {
        Time.timeScale = 1;
        stageCleared = true;
        fadeOut.FadingOut();
        source.volume -= Time.deltaTime / fadeOutTime;
        source.volume -= 0.0000001f;

        yield return new WaitForSecondsRealtime(fadeOutTime);
        GameData.CurrentScene = nextScene;
        SceneManager.LoadScene(nextScene);

        player.ResetPlayer();
        source.volume = 0;
        source.Play();
        source.volume += .0000001f;

        if (source.volume > startingVolume) source.volume = startingVolume;
        prize.isRescued = false;

    }

    IEnumerator LoadAltNextScene()
    {
        Time.timeScale = 1;
        stageCleared = true;
        fadeOut.FadingOut();
        source.volume -= Time.deltaTime / fadeOutTime;
        source.volume -= 0.0000001f;

        yield return new WaitForSecondsRealtime(fadeOutTime);
        GameData.CurrentScene = altNextScene;
        SceneManager.LoadScene(altNextScene);

        player.ResetPlayer();
        source.volume = 0;
        source.Play();
        source.volume += .0000001f;

        if (source.volume > startingVolume) source.volume = startingVolume;
        prize.isRescued = false;

    }

    IEnumerator LoadCompletionistScene()
    {
        Time.timeScale = 1;
        stageCleared = true;
        fadeOut.FadingOut();
        source.volume -= Time.deltaTime / fadeOutTime;
        source.volume -= 0.0000001f;

        yield return new WaitForSecondsRealtime(fadeOutTime);
        GameData.CurrentScene = completionistScene;
        SceneManager.LoadScene(completionistScene);

        player.ResetPlayer();
        source.volume = 0;
        source.Play();
        source.volume += .0000001f;

        if (source.volume > startingVolume) source.volume = startingVolume;
        prize.isRescued = false;
        GameData.TotalScrap = 0;
        GameData.Completionist = false;



    }

}

