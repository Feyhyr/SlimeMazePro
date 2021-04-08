using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject winScreen;
    [Space(10)]
    public List<GameObject> coins;

    public AudioClip audioSFX;

    public void PlayAudio()
    {
        AudioManager.Instance.Play(audioSFX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayAudio();
            winScreen.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        winScreen.SetActive(false);

        GameObject player = GameObject.Find("Player");
        PlayerMove p = player.GetComponent<PlayerMove>();

        GameObject gm = GameObject.Find("GameManager");
        GameManager gameMngr = gm.GetComponent<GameManager>();
        
        if (gameMngr.isLevel1)
        {
            PlayerPrefs.SetInt("prefScore", 0);
        }

        else if (gameMngr.isLevel2)
        {
            PlayerPrefs.SetInt("prefScore", p.tempNum);
        }

        gameMngr.totalCoins = PlayerPrefs.GetInt("prefScore");

        p.controls = true;
    }
}
