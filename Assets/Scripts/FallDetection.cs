using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;



public class DeathFall : MonoBehaviour

{

    public int maxHealth = 3;
    private int currentHealth;

    public TMP_Text healthText; // Zmiana na TextMeshProUGUI.

    void Update()
    {
        if (gameObject.transform.position.y <= 0) //detekcja upadku ustawiona na -10
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //przeładowanie sceny
            SC_CoinCounter.totalCoins = 0; //reset stanu monet
        }
    }

}
