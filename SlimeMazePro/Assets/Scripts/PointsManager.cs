using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public Text pointsText;

    private GameObject gm;
    private GameManager gameMngr;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        gameMngr = gm.GetComponent<GameManager>();
    }

    private void Update()
    {
        pointsText.text = gameMngr.totalCoins.ToString();
    }
}
