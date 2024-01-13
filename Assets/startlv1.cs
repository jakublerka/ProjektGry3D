using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startlv1 : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("level1");
    }

    public void startScene()
    {
        SceneManager.LoadScene("level1");

    }
}