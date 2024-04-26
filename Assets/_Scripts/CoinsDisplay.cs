using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinsDisplay : MonoBehaviour
{
    public TMP_Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinSystem playerCoinSystem = FindObjectOfType<CoinSystem>();
        if(playerCoinSystem != null)
        {
            coinsText.text = "Current Coins: " + playerCoinSystem.currentCoins.ToString();
        }
        
    }
}
