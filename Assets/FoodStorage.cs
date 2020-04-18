using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour
{
    private List<float> foodAmounts = new List<float>();
    public void AddFood(Food food)
    {
        foodAmounts.Add(food.amount);
    }

    public float RemoveFood()
    {
        var foodAmount = foodAmounts[0];
        foodAmounts.RemoveAt(0);
        return foodAmount;
    }
}
