using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFall : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.position.y <= 0) //detekcja upadku ustawiona na -10
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //przeładowanie sceny
            SC_CoinCounter.totalCoins = 0; //reset stanu monet
        }
    }
}
