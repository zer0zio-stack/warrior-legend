using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    public Image healthBar;
    public Image healthBarRed;
    public Image powerBar;

    private void Update()
    {
        if (healthBarRed.fillAmount > healthBar.fillAmount) healthBarRed.fillAmount -= Time.deltaTime / 2;
    }

    public void OnHealthBarChange(float rate)
    {
        healthBar.fillAmount = rate;
    }

    public void OnPowerBarChange(float arg0CurrentPower)
    {
        powerBar.fillAmount = arg0CurrentPower;
    }
}