using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void recordEvent(string eventname)
    {
        // event name = level complete
        Analytics.CustomEvent(eventname);
    }

    public void recordEvent(string eventname, string key, string value)
    {
        // eventname = spell, key = water, walue = 40%
        // eventname = skinsPurchased, key = character, value = skin2
        Dictionary<string, object> eventParams = new Dictionary<string, object>();
        eventParams.Add(key, value);
        Analytics.CustomEvent(eventname, eventParams);

    }
}