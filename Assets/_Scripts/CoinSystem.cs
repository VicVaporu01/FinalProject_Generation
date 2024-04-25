using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public int startingCoins = 100;
    public int currentCoins;
    // Start is called before the first frame update
    void Start()
    {
        currentCoins = startingCoins;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GainCoins(int amount)
    {
        currentCoins += amount;
    }

    public void LoseCoins(int amount)
    {
        currentCoins -= amount;
        if (currentCoins < 0)
            currentCoins = 0;
    }

}
