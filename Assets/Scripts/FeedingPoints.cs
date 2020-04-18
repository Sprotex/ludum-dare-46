using System.Collections.Generic;
using UnityEngine;

public class FeedingPoints : MonoBehaviour
{
    public List<Food> food = new List<Food>();

    public static FeedingPoints instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
