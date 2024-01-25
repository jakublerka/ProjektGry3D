using UnityEngine;

public class CoinRotationAnimation : MonoBehaviour
{
    public string coinTag = "Coin";
    public float rotationSpeed = 180.0f; // Pr�dko�� obrotu

    void Update()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag(coinTag);

        foreach (GameObject coin in coins)
        {
            // Zastosuj animacj� obrotu
            coin.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Sprawd�, czy obiekt obr�ci� si� o pe�ne 360 stopni
            if (coin.transform.rotation.eulerAngles.y >= 360.0f)
            {
                // Zresetuj rotacj�
                coin.transform.rotation = Quaternion.identity;
            }
        }
    }
}
