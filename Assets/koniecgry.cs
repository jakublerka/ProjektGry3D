using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "level3")
            {
                SceneManager.LoadScene("YouWin");
            }
        }
    }
}
