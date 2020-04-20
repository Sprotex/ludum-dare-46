using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodStorage : MonoBehaviour
{
    private List<float> foodAmounts = new List<float>();
    private float totalFood = 0f;
    private const string format = "F0";
    public TextMeshProUGUI foodTextUI;
    public void AddFood(Food food)
    {
        SoundManager.instance.Play(transform.position, SoundManager.instance.wormPickup);
        foodAmounts.Add(food.amount);
        totalFood += food.amount;
        foodTextUI.SetText(totalFood.ToString(format));
    }

    private float RemoveFood()
    {
        var result = 0f;
        if (foodAmounts.Count > 0)
        {
            result = foodAmounts[0];
            foodAmounts.RemoveAt(0);
            totalFood -= result;
            foodTextUI.SetText(totalFood.ToString(format));
        } 
        return result;
    }

    public void UseFood(Nest nest)
    {
        nest.Feed(RemoveFood());
    }

    public void UseFood(Health health)
    {
        health.HealthPercentage += RemoveFood();
    }
}
