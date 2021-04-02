using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        winScreen.SetActive(false);

        GameObject player = GameObject.Find("Player");
        PlayerMove p = player.GetComponent<PlayerMove>();
        p.controls = true;
    }
}
