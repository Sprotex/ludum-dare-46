using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nest : MonoBehaviour
{
    private float hungerPercentage = 1f;
    public float hungerDecrementPerSecond = 0.01f;
    public float hungerDecrementAdd = 0.01f;
    public Image hungerIndicator;

    // NOTE(Andy): Property, because it will be integrated with the UI.
    public float HungerPercentage
    {
        get
        {
            return hungerPercentage;
        }
        set
        {
            hungerPercentage = value;
            hungerIndicator.fillAmount = value;
        }
    }

    private IEnumerator HungerIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            hungerDecrementPerSecond += hungerDecrementAdd; 
        }
    }

    private void Start()
    {
        StartCoroutine(HungerIncrease());
    }

    public void Update()
    {
        HungerPercentage -= hungerDecrementPerSecond * Time.deltaTime;
    }

    public void Feed(float percentageAmount)
    {
        HungerPercentage = Mathf.Clamp01(HungerPercentage + percentageAmount);
    }
}
