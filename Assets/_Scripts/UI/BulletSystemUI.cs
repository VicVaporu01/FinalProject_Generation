using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BulletSystemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider counterSlider;
    [SerializeField] private TextMeshProUGUI bulletAmountText;
    [SerializeField] private PlayerShoot playerShoot;

    private void Start()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();

        InitializeSliderValues(playerShoot.timeToChargeAmmo);

        playerShoot.OnBulletAmountChange.AddListener(ChangeBulletAmountText);

        playerShoot.OnBulletRechargeChange.AddListener(CahngeSliderRechargeValue);
    }

    private void CahngeSliderRechargeValue(float rechargeValue)
    {
        counterSlider.value = rechargeValue;
    }

    private void InitializeSliderValues(float timeToRecharge)
    {
        counterSlider.maxValue = timeToRecharge;
    }

    private void ChangeBulletAmountText(int bulletAmount)
    {
        bulletAmountText.text = bulletAmount.ToString();
    }
}
