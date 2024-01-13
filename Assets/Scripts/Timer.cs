using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public Transform player;

    //Do zarzadzania czy level ma byc na czas, defaultowo nie.
    //public bool timeRunning;
    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time running down: "+timeRunning);
        /*
        if(timeRunning==true)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
        }*/

        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (player != null)
            {
                Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);
                
                //timerText.rectTransform.anchoredPosition = new Vector2(screenPos.x, Screen.height - screenPos.y);
            }
        } else {
            timerText.text="Koniec czasu!";
        }

        DisplayTime(timeRemaining);
        //Debug.Log("Time remeaning: "+timeRemaining);
    }

    void DisplayTime(float timeRemaining)
    {   
        this.timeRemaining = timeRemaining;
        float minuty = Mathf.FloorToInt(timeRemaining / 60);
        float sekundy = Mathf.FloorToInt(timeRemaining % 60);
        string timeToDisplay = string.Format("Czas: {0:00}:{1:00}", minuty, sekundy);
        timerText.text = timeToDisplay;
    }
}
