using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneLoadLevel()
    {
        GameObject gm = GameObject.Find("GameManager");
        GameManager gameMngr = gm.GetComponent<GameManager>();

        if (gameMngr.isLevel1)
        {
            SceneManager.LoadScene("Level1Scene");
        }
        else if (gameMngr.isLevel2)
        {
            SceneManager.LoadScene("Level2Scene");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Game has quit!");
        Application.Quit();
    }

}
