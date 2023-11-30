using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 100f;

    //Do zarzadzania czy level ma byc na czas, defaultowo nie.
    public bool timeRunning = false;

    // Update is called once per frame
    void Update()
    {
        float minuty = Mathf.FloorToInt(timeRemaining / 60);
        float sekundy = Mathf.FloorToInt(timeRemaining % 60);
        if(timeRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
        }
    }

    void DisplayTime()
    {

    }
}
