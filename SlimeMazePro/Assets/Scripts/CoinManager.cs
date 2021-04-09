using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public List<GameObject> starPosition;
    public GameObject prefab;

    private GameObject coinObject;
    public int index = 0;

    private void Start()
    {
        coinObject = Instantiate(prefab, starPosition[0].transform);
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
        coinObject.transform.position = starPosition[index].transform.position;
    }
}
