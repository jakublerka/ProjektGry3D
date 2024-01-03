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
    public TextMeshPro timerText;

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
                // Convert player's position to screen space
                Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);
                
                // Set the position of the timer to the top left corner
                timerText.rectTransform.anchoredPosition = new Vector2(screenPos.x, Screen.height - screenPos.y);
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
        timerText.text = string.Format("Time: {0:00}:{1:00}", minuty, sekundy);
    }
}
