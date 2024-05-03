using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class CoinsDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    [SerializeField] private CoinSystem playerCoinSystem;

    private void Awake()
    {
        playerCoinSystem = FindObjectOfType<CoinSystem>();

        if (playerCoinSystem != null)
        {
            playerCoinSystem.OnCoinsChanged.AddListener(ChangeCoinTextUI);
        }
    }

    private void ChangeCoinTextUI(int actualCoinAmount)
    {
        coinsText.text = actualCoinAmount.ToString();
    }
}
