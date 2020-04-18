using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float hungerDecrement = 0.1f;
    public float hungryThreshold = 0.4f;
    public float huntingSpeed = 15f;

    // Height of idle flight
    public float flightHeight = 20.0f;

    // Only look at worms that are not that close
    public float minimumTargetDistance = 30.0f;

    // Fly to the ground, when object is this close (not counting Y axis in the distance)
    public float groundAttackDistance = 30.0f;
    
    public float angularVelocity = 60f;

    // Current target
    public Transform targetPosition = null;
    public Food targetObject = null;


    private float hungerPercentage = 0f;

    // Waypoints used, when not attacking a worm
    private Vector3 waypoint = new Vector3(0.0f, 0.0f, 0.0f);
    float timeToNextWaypoint = 5.0f;
    float waypointAreaRange = 250.0f;

    // The closest it ever was to it's current target
    private float closesDistanceToTarget = float.PositiveInfinity;

    private void Update()
    {
        if (hungerPercentage <= hungryThreshold) {
            if (targetPosition == null)
            {
                // No target -> find some
                FlyToPosition(new Vector3(0.0f, 0.0f, 0.0f), true);
                FindTarget();
            } 
            else 
            {
                // Positions without Y axis
                Vector2 myPosition2D = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetPosition2D = new Vector2(targetPosition.position.x, targetPosition.position.z);

                // Distance to target with and withnout Y axis
                float distanceToTarget2D = (myPosition2D - targetPosition2D).magnitude;
                float distanceToTarget3D = (transform.position - targetPosition.position).magnitude;

                closesDistanceToTarget = Mathf.Min(closesDistanceToTarget, distanceToTarget2D);

                FlyToPosition(targetPosition.position, distanceToTarget2D > groundAttackDistance);

                // Worm taken
                if ((transform.position - targetPosition.position).sqrMagnitude < 5.0f)
                {
                    Destroy(targetObject.gameObject);
                    FindTarget();
                    HeadUp();
                    hungerPercentage = 1.0f;
                // Counld not hit this worm
                }else if (closesDistanceToTarget * 1.5 < distanceToTarget2D)
                {
                    FindTarget();
                }
            }
        }else
        {
            // Idle mode, follow waypoints
            timeToNextWaypoint -= Time.deltaTime;
            if (timeToNextWaypoint < 0.0f)
            {
                // New waypoint
                timeToNextWaypoint = 6.0f;
                float x = Random.Range(-waypointAreaRange, waypointAreaRange);
                float y = Random.Range(-waypointAreaRange, waypointAreaRange);
                waypoint = new Vector3(x, y, flightHeight);
            }
            FlyToPosition(waypoint, true);
        }


        hungerPercentage = Mathf.Clamp01(hungerPercentage - hungerDecrement * Time.deltaTime);
    }

    private void HeadUp()
    {
        Vector3 direction = transform.rotation.eulerAngles;
        direction.y = Mathf.Abs(direction.y);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void FlyToPosition(Vector3 targetPosition, bool stayHigh)
    {
        // Do not fly to ground, stay in the air
        if (stayHigh)
            targetPosition.y = flightHeight;

        // Get direction to target
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Slowly change direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * angularVelocity);

        // Move the object
        transform.position += transform.forward * Time.deltaTime * huntingSpeed;

    }

    private void FindTarget()
    {
        Object originalObject = targetObject;

        targetObject = null;
        targetPosition = null;
        closesDistanceToTarget = float.PositiveInfinity;


        // find a target
        var closestDistance = float.PositiveInfinity;
        foreach (var food in FeedingPoints.instance.food)
        {
            var diff = transform.position - food.transform.position;
            var magnitude = diff.magnitude;
            if (magnitude < closestDistance && magnitude > minimumTargetDistance && food != originalObject)
            {
                targetObject = food;
                targetPosition = food.transform;
                closestDistance = magnitude;
            }
        }
    }
}
