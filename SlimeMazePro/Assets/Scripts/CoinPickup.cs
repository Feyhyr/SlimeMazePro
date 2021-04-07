using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);

            GameObject gm = GameObject.Find("GameManager");
            GameManager gameMngr = gm.GetComponent<GameManager>();
            gameMngr.GetCoin();
        }
    }
}
