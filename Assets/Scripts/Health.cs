﻿using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float healthPercentage = 1f;
    public Image healthIndicator;
    public DeathUI deathUI;

    // NOTE(Andy): Property, because it will be integrated with the UI.
    public float HealthPercentage
    {
        get
        {
            return healthPercentage;
        }
        set
        {
            if (healthPercentage > value)
            {
                SoundManager.instance.Play(transform.position, SoundManager.instance.playerHit);
            }
            healthPercentage = value;
            healthIndicator.fillAmount = value;

            if (HealthPercentage <= 0f)
            {
                deathUI.DeathByHealth();
            }
        }
    }

    private void Update()
    {
        if (HealthPercentage <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
