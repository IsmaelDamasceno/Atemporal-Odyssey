using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    private static int coinAmount;
    private TextMeshProUGUI text;
    private static CoinController instance;

    void Start()
    {
        if (instance == null)
        {
            text = GetComponent<TextMeshProUGUI>();
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }

    public static void GetCoins(int amount)
    {
        coinAmount += amount;
        instance.text.text = coinAmount.ToString();
    }
}
