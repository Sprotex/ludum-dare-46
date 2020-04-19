using UnityEngine;

public class PlayerFeed : MonoBehaviour
{
    public FoodStorage foodStorage;
    public Health health;
    public Nest nest;
    public float nestFeedingRadius = 3f;

    private void Update()
    {
        if (Input.GetButtonDown(CConstants.Input.FeedSelf))
        {
            foodStorage.UseFood(health);
        }
        if (Input.GetButtonDown(CConstants.Input.FeedChildren))
        {
            var diff = nest.transform.position - transform.position;
            if (diff.sqrMagnitude <= nestFeedingRadius * nestFeedingRadius)
            {
                foodStorage.UseFood(nest);
            }
        }
    }
}
