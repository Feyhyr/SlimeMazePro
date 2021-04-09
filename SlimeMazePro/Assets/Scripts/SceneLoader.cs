using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject gm;
    private GameManager gameMngr;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        gameMngr = gm.GetComponent<GameManager>();
    }

    public void SceneLoad(string sceneName)
    {
        if (sceneName == "Level2Scene")
        {
            gameMngr.tempHearts = gameMngr.currentLives;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void SceneLoadLevel()
    {
        if (gameMngr.isLevel1)
        {
            SceneManager.LoadScene("Level1Scene");
        }
        else if (gameMngr.isLevel2)
        {
            gameMngr.tempHearts = gameMngr.currentLives;
            SceneManager.LoadScene("Level2Scene");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Game has quit!");
        Application.Quit();
    }

}
