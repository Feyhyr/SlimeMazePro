using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        if (sceneName == "Level2Scene")
        {
            GameObject player = GameObject.Find("Player");
            PlayerMove p = player.GetComponent<PlayerMove>();
            p.controls = true;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Game has quit!");
        Application.Quit();
    }

}
