using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalCoins;
    public int currentLevelCoins;
    public int totalLives;
    public int currentLives;

    public Text livesText;

    public GameObject player;
    PlayerMove p;

    static GameManager instance;

    public const string prefLives = "prefLives";

    public const string prefScore = "prefScore";

    public bool isLevel1;
    public bool isLevel2;

    public int tempHearts;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (!(PlayerPrefs.HasKey(prefLives)))
        {
            PlayerPrefs.SetInt(prefLives, 2);
        }
        totalLives = PlayerPrefs.GetInt(prefLives);
        currentLives = totalLives;

        PlayerPrefs.SetInt(prefScore, 0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        p = player.GetComponent<PlayerMove>();
    }

    public void GetCoin()
    {
        currentLevelCoins += 50;
    }

    public void LoseLive()
    {
        currentLives--;

        if (currentLives == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void IncreaseMaxLives()
    {
        totalLives++;
        currentLives = totalLives;
        PlayerPrefs.SetInt(prefLives, totalLives);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        livesText = GameObject.Find("LivesNumTxt").GetComponent<Text>();

        if (scene.name == "MainMenuScene")
        {
            player.SetActive(false);
            currentLives = totalLives;
            totalCoins = 0;
            isLevel1 = false;
            isLevel2 = false;
        }

        else if (scene.name == "Level1Scene" || scene.name == "Level2Scene")
        {
            player.SetActive(true);
            p.controls = true;
        }

        if (scene.name == "Level1Scene")
        {
            currentLives = totalLives;
            isLevel1 = true;
        }

        if (scene.name == "Level2Scene")
        {
            isLevel1 = false;
            isLevel2 = true;
        }
    }

    private void LateUpdate()
    {
        livesText.text = currentLives.ToString();
    }
}
