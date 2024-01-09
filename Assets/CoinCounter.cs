using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_CoinCounter : MonoBehaviour
{
    public static int totalCoins = 0;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            GameObject coinCounterObject = GameObject.Find("CoinCounter");
            TextMeshProUGUI textComponent = coinCounterObject.GetComponent<TextMeshProUGUI>();
            totalCoins++;
            Debug.Log("Ilosc monet " + SC_CoinCounter.totalCoins);
            Destroy(gameObject);
            GameObject text = GameObject.Find("CoinText");
            textComponent.text = totalCoins.ToString();
        }
    }
}