using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSystem : MonoBehaviour
{
    public int currentCoins;

    [Header("Events")]
    public UnityEvent<int> OnCoinsChanged;

    void Start()
    {
        currentCoins = GameManager.Instance.coinsAmount;

        OnCoinsChanged.Invoke(currentCoins);
    }

    public void GainCoins(int amount)
    {
        currentCoins += amount;

        OnCoinsChanged.Invoke(currentCoins);

        GameManager.Instance.coinsAmount = currentCoins;
    }

    public void LoseCoins(int amount)
    {
        currentCoins -= amount;

        if (currentCoins < 0)
        {
            currentCoins = 0;
        }

        OnCoinsChanged.Invoke(currentCoins);

        GameManager.Instance.coinsAmount = currentCoins;
    }

}
