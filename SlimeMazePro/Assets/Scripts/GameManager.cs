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

    public Text livesText;

    public GameObject player;

    static GameManager instance;

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
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void GetCoin()
    {
        currentLevelCoins++;
    }

    public void LoseLive()
    {
        totalLives -= 1;

        if (totalLives == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        livesText = GameObject.Find("LivesNumTxt").GetComponent<Text>();

        if (scene.name == "MainMenuScene")
        {
            player.SetActive(false);
            totalLives = 2;
        }

        else if (scene.name == "Level1Scene" || scene.name == "Level2Scene")
        {
            player.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        livesText.text = totalLives.ToString();
    }
}
