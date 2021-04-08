using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PurchaseManager : MonoBehaviour
{
    private GameObject gm;
    private GameManager gameMngr;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        gm = GameObject.Find("GameManager");
        gameMngr = gm.GetComponent<GameManager>();
    }

    public void LivesCaller()
    {
        gameMngr.IncreaseMaxLives();
    }

}
