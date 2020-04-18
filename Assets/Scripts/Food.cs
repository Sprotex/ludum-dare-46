using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float amount = 0.1f;
    private void Start()
    {
        FeedingPoints.instance.food.Add(this);
    }

    private void OnDestroy()
    {
        FeedingPoints.instance.food.Remove(this);
    }
}
