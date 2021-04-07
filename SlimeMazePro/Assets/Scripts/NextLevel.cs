using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject winScreen;
    [Space(10)]
    public List<GameObject> coins;

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
