using UnityEngine;

public class CoinRotationAnimation : MonoBehaviour
{
    public string coinTag = "Coin";
    public float rotationSpeed = 180.0f; // Prêdkoœæ obrotu

    void Update()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag(coinTag);

        foreach (GameObject coin in coins)
        {
            // Zastosuj animacjê obrotu
            coin.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // SprawdŸ, czy obiekt obróci³ siê o pe³ne 360 stopni
            if (coin.transform.rotation.eulerAngles.y >= 360.0f)
            {
                // Zresetuj rotacjê
                coin.transform.rotation = Quaternion.identity;
            }
        }
    }
}
