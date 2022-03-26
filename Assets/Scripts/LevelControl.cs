using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviourSingletonPersistent<LevelControl>
{
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Text retryCounterText;
    [SerializeField] private string[] levelNames;
    [SerializeField] private Text announcerText;

    private Coroutine announcerRoutine;
    private bool controlEnabled = true;
    public bool ControlEnabled { get => controlEnabled;  }

    private bool isPaused = false;
    private bool canPause = false;

    private int currentLevel = 0;
    private int retryCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ResetLevel();
        if (Input.GetKeyDown(KeyCode.Escape) && canPause) Pause();
    }

    public void OnLevelLoad()
    {
        controlEnabled = true;
        isPaused = false;
        defeatScreen.SetActive(false);
        victoryScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        if (currentLevel >= 1) canPause = true;
        else canPause = false;
        retryCounterText.text = "Tentativas: " + retryCounter;
    }


    public void ResetLevel()
    {
        SceneManager.LoadScene(levelNames[currentLevel]);
        retryCounter++;
        retryCounterText.text = "Tentativas: " + retryCounter;
    }

    public void Death()
    {
        canPause = false;
        controlEnabled = false;
        defeatScreen.SetActive(true);

    }

    public void Victory()
    {
        canPause = false;
        controlEnabled = false;
        victoryScreen.SetActive(true);
    }

    public void Pause()
    {
        if (!canPause) return;
        isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);
        controlEnabled = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(levelNames[currentLevel]);
    }

    public void StartGame()
    {
        currentLevel = 1;
        SceneManager.LoadScene(levelNames[currentLevel]);
        retryCounter = 0;
    }

    public void GoToMenu()
    {
        isPaused = false;
        defeatScreen.SetActive(false);
        victoryScreen.SetActive(false);
        pauseScreen.SetActive(false);
        currentLevel = 0;
        controlEnabled = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(levelNames[currentLevel]);

    }

    public void CallAnnouncer(string content)
    {
        announcerText.gameObject.SetActive(true);
        announcerText.text = content;
        if (announcerRoutine != null) StopCoroutine(announcerRoutine);
        announcerRoutine = StartCoroutine(AnnouncerLifetime());
    }

    public IEnumerator AnnouncerLifetime()
    {
        yield return new WaitForSeconds(5f);
        announcerText.gameObject.SetActive(false);
    }


    
}
