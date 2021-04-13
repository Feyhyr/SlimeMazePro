using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public List<Transform> starPosition;
    public GameObject starPrefab;

    private GameObject coinObject;
    public int index = 0;

    private void Start()
    {
        coinObject = Instantiate(starPrefab, starPosition[0]);
    }

    public void ChangeIndex()
    {
        index++;
        if (index > starPosition.Count - 1)
        {
            index = 0;
        }
    }

    public void PickupEvent()
    {
        coinObject.transform.position = starPosition[index].position;
    }
}
