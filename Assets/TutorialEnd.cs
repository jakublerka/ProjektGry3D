using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchObject : MonoBehaviour
{
    public GameObject panel; // Przypisz obiekt Panel w inspektorze
    public TextMeshProUGUI textMeshPro; // Przypisz obiekt TextMeshProUGUI w inspektorze

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Wy�wietl napis "Tutorial zaliczony" na p�przezroczystym tle
            panel.SetActive(true);
            textMeshPro.text = "Tutorial zaliczony";
        }
    }
}
