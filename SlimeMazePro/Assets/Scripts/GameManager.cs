using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalCoins;
    public int currentLevelCoins;
    public int totalLives = 2;
    public int currentLives;

    public Text livesText;

    public GameObject player;

    static GameManager instance;

    public const string prefLives = "prefLives";

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
        //PlayerPrefs.SetInt("prefLives", 2);
        totalLives = PlayerPrefs.GetInt(prefLives);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public void GetCoin()
    {
        currentLevelCoins++;
    }

    public void LoseLive()
    {
        currentLives -= 1;

        if (currentLives == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void IncreaseMaxLives()
    {
        totalLives += 1;
        PlayerPrefs.GetInt(prefLives, totalLives);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        livesText = GameObject.Find("LivesNumTxt").GetComponent<Text>();

        if (scene.name == "MainMenuScene")
        {
            player.SetActive(false);
            currentLives = totalLives;
        }

        else if (scene.name == "Level1Scene" || scene.name == "Level2Scene")
        {
            player.SetActive(true);
        }

        if (scene.name == "Level1Scene")
        {
            currentLives = totalLives;
        }
    }

    private void LateUpdate()
    {
        livesText.text = currentLives.ToString();
    }
}
