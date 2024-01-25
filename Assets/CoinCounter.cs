using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_CoinCounter : MonoBehaviour
{
    public static int totalCoins = 0;
    public float collectAnimationDuration = 0.5f; // Skr�cony czas trwania animacji podnoszenia (p� sekundy)

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    IEnumerator CollectCoinAndDestroy()
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0.0f, 1.0f, 0.0f); // Przesuni�cie w g�r� o 1 jednostk�

        while (elapsedTime < collectAnimationDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / collectAnimationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Zniszcz monet� po zako�czeniu animacji
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            // Uruchom animacj� podnoszenia monety
            StartCoroutine(CollectCoinAndDestroy());

            // Zaktualizuj licznik monet
            GameObject coinCounterObject = GameObject.Find("CoinCounter");
            TextMeshProUGUI textComponent = coinCounterObject.GetComponent<TextMeshProUGUI>();
            totalCoins++;
            Debug.Log("Ilo�� monet: " + SC_CoinCounter.totalCoins);
            GameObject text = GameObject.Find("CoinText");
            textComponent.text = totalCoins.ToString();
        }
    }
}
