using UnityEngine;

public class PlayerFeed : MonoBehaviour
{
    public FoodStorage foodStorage;
    public Health health;
    public Nest nest;
    public float nestFeedingRadius = 3f;

    private void Update()
    {
        if (Input.GetButtonDown("Feed Self"))
        {
            print("Feed self");
            foodStorage.UseFood(health);
        }
        if (Input.GetButtonDown("Feed Children"))
        {
            print("Feed children");
            var diff = nest.transform.position - transform.position;
            if (diff.sqrMagnitude <= nestFeedingRadius * nestFeedingRadius)
            {
                foodStorage.UseFood(nest);
            }
        }
    }
}
