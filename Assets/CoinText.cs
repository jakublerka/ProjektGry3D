using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_CoinText : MonoBehaviour
{
    Text counterText;

    void Start()
    {
        counterText = GetComponent<Text>();
    }

    void Update()
    {
        counterText.text = SC_CoinCounter.totalCoins.ToString();
    }
}