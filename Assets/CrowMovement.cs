using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float hungerDecrement = 0.1f;
    public float hungryThreshold = 0.4f;
    public float huntingSpeed = 5f;

    public Transform target = null;
    private float hungerPercentage = 1f;

    private void Update()
    {
        if (hungerPercentage <= hungryThreshold) {
            if (target == null)
            {
                // find a target
                var closestSqrDistance = float.PositiveInfinity;
                foreach (var food in FeedingPoints.instance.food)
                {
                    var diff = transform.position - food.transform.position;
                    var sqrMagnitude = diff.sqrMagnitude;
                    if (sqrMagnitude < closestSqrDistance) {
                        target = food.transform;
                        closestSqrDistance = sqrMagnitude;
                    }
                }
            } else 
            {
                // approach your target
                var direction = target.position - transform.position;
                var translation = direction.normalized * Time.deltaTime * huntingSpeed;
                transform.Translate(translation);
            }
        }
        hungerPercentage = Mathf.Clamp01(hungerPercentage - hungerDecrement * Time.deltaTime);
    }
}
