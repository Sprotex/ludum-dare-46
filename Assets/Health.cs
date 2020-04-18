using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float healthPercentage = 1f;
    public Image healthIndicator;

    // NOTE(Andy): Property, because it will be integrated with the UI.
    public float HealthPercentage
    {
        get
        {
            return healthPercentage;
        }
        set
        {
            healthPercentage = value;
            healthIndicator.fillAmount = value;
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
