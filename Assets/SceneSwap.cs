using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    private void OnGUI()
    {
        int xCenter = (Screen.width / 2);
        int yCenter = (Screen.height / 2);
        int width = 200;
        int height = 50;

        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("button"));
        fontSize.fontSize = 32;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "level1")
        {
            if (GUI.Button(new Rect(Screen.width - width*2 / 2, Screen.height - height*2 / 2, width, height), "Next lvl", fontSize))
            {
                SceneManager.LoadScene("level2");
            }
        }
        else if (scene.name == "level2")
        {
            if (GUI.Button(new Rect(Screen.width - width * 2 / 2, Screen.height - height * 2 / 2, width, height), "Next lvl", fontSize))
            {
                SceneManager.LoadScene("level3");
            }
        }
        else if (scene.name == "level3")
        {
            if (GUI.Button(new Rect(Screen.width - width * 2 / 2, Screen.height - height * 2 / 2, width, height), "Next lvl", fontSize))
            {
                SceneManager.LoadScene("level1");
            }
        }
    }
}