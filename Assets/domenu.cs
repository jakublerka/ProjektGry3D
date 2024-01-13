using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class domenu : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("menu");
    }

    public void startScene()
    {
        SceneManager.LoadScene("menu");

    }
}