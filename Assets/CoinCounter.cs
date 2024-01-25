using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_CoinCounter : MonoBehaviour
{
    public static int totalCoins = 0;
    public float collectAnimationDuration = 0.5f; // Skrócony czas trwania animacji podnoszenia (pó³ sekundy)

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    IEnumerator CollectCoinAndDestroy()
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0.0f, 1.0f, 0.0f); // Przesuniêcie w górê o 1 jednostkê

        while (elapsedTime < collectAnimationDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / collectAnimationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Zniszcz monetê po zakoñczeniu animacji
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            // Uruchom animacjê podnoszenia monety
            StartCoroutine(CollectCoinAndDestroy());

            // Zaktualizuj licznik monet
            GameObject coinCounterObject = GameObject.Find("CoinCounter");
            TextMeshProUGUI textComponent = coinCounterObject.GetComponent<TextMeshProUGUI>();
            totalCoins++;
            Debug.Log("Iloœæ monet: " + SC_CoinCounter.totalCoins);
            GameObject text = GameObject.Find("CoinText");
            textComponent.text = totalCoins.ToString();
        }
    }
}
