using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_CoinText : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        counterText.text = SC_CoinCounter.totalCoins.ToString();
    }
}