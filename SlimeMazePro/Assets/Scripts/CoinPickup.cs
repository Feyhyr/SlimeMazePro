using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioClip audioSFX;

    public void PlayAudio()
    {
        AudioManager.Instance.Play(audioSFX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayAudio();
            gameObject.SetActive(false);

            GameObject gm = GameObject.Find("GameManager");
            GameManager gameMngr = gm.GetComponent<GameManager>();
            gameMngr.GetCoin();
        }
    }
}
