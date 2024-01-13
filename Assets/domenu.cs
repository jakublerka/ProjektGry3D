using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class domenu : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void startScene()
    {
        SceneManager.LoadScene("Menu");

    }
}