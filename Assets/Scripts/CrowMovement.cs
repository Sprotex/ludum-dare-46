using UnityEngine;

public class CrowMovement : MonoBehaviour
{

    public enum State
    {
        // Flyting around, when timer is out, switch to APPROACHING
        IDLE,

        // Flying closer to a worm. When you get close enough, do WORM_ATTACK
        APPROACHING,

        // Fly down from the sky to the worm. When you get it, switch to WORM_EATING
        WORM_ATTACK,

        // Staying on the ground, eating the worm, then RETURN_TO_AIR. If attacked by player, go to WALK_TO_PLAYER
        WORM_EATING,

        // Get back to air, then go back to IDLE
        RETURN_TO_AIR,

        // Attack player. If player is too far away, WALK_TO_PLAYER
        COMBAT,

        // Get closer to the player by walking (used in combat). If close enough, go to COMBAT. If the player is away for too long, RETURN_TO_AIR 
        WALK_TO_PLAYER,
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            if (value < health)
            {
                if (state == State.WORM_EATING)
                {
                    timer = 5.0f;
                    state = State.WALK_TO_PLAYER;
                }
            }

            health = value;
        }
    }

    private float health;

    public Animator animator;
    public Health playerHealth = null;

    public State state = State.IDLE;
    // Universal timer for various timeouts
    public float timer = 0.0f;

    private float timeToNextAttack = 0.0f;

    public float attackDmg = 0.1f;
    public float attackDelay = 0.5f;

    public float hungerTimeout = 12.0f;


    public float flightSpeed = 20f;
    public float walkSpeed = 10f;

    // Height of idle flight
    public float flightHeight = 20.0f;

    // Only look at worms that are not that close
    public float minimumTargetDistance = 40.0f;

    // Fly to the ground, when object is this close (not counting Y axis in the distance)
    public float groundAttackDistance = 10.0f;

    public float angularVelocity = 30f;

    // Current target
    public Transform targetPosition = null;
    public Food targetObject = null;


    // Waypoints used, when not attacking a worm
    private Vector3 waypoint = new Vector3(0.0f, 0.0f, 0.0f);
    public float waypointAreaRange = 250.0f;

    private void Update()
    {
        timer -= Time.deltaTime;

        switch (state)
        {
            case State.IDLE:
            {
                float distToWaypoint = (transform.position - waypoint).magnitude;
                if (distToWaypoint < 50.0f)
                {
                    GenerateWaypoint();
                }

                // Attack worm
                if (timer <= 0)
                {
                    FindTarget();
                }

                FlyToPosition(waypoint, true, 1.0f);
            }

            break;

            case State.APPROACHING:
            {
                // Positions without Y axis
                Vector2 myPosition2D = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetPosition2D = new Vector2(targetPosition.position.x, targetPosition.position.z);

                // Distance to target withnout Y axis
                float distanceToTarget2D = (myPosition2D - targetPosition2D).magnitude;


                // Kamikadze
                if (distanceToTarget2D < groundAttackDistance)
                {
                    state = State.WORM_ATTACK;
                }

                FlyToPosition(targetPosition.position, true, 2.0f);
            }
            break;

            // Kamikadze
            case State.WORM_ATTACK:
            {
                float distanceToTarget3D = (transform.position - targetPosition.position).magnitude;


                if (transform.position.y <= targetPosition.position.y + 1.0f)
                {
                    Destroy(targetObject.gameObject);

                    // ANIMATION EATING
                    animator.SetTrigger(CConstants.Animator.CrowEating);

                    state = State.WORM_EATING;
                    timer = 4.0f;
                }

                FlyToPosition(targetPosition.position, false, 8.0f);
            }

            break;

            case State.WORM_EATING:
            {
                // Return back to air
                if (timer <= 0)
                {
                    ReturnToAir();
                }

                // If player is close, attack it
                Vector3 playerPosition = playerHealth.gameObject.transform.position;
                float playerDistance = (playerPosition - transform.position).magnitude;
                if (playerDistance < 5.0f)
                {
                    state = State.WALK_TO_PLAYER;
                    timer = 5.0f;

                    // ANIMATION walk
                    animator.SetTrigger(CConstants.Animator.CrowWalk);
                }


                // Stand straingt
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.x = rotation.z = 0.0f;
                Quaternion rotQ = new Quaternion();
                rotQ.eulerAngles = rotation;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotQ, Time.deltaTime * 180.0f);

            }
            break;

            case State.RETURN_TO_AIR:
            {
                // Returned back to air
                if (transform.position.y >= flightHeight - 5.0)
                {
                    state = State.IDLE;
                    timer = hungerTimeout;
                    GenerateWaypoint();
                }

                FlyToPosition(waypoint, true, 8.0f);
            }
            break;

            case State.COMBAT:
            {
                // Player not specified
                if (playerHealth == null)
                {
                    state = State.RETURN_TO_AIR;
                    return;
                }

                Vector3 playerPosition = playerHealth.gameObject.transform.position;
                float playerDistance = (playerPosition - transform.position).magnitude;

                RotateToPlayer();

                if (playerDistance <= 3.0)
                {
                    timeToNextAttack -= Time.deltaTime;
                    if (timeToNextAttack <= 0)
                    {
                        timeToNextAttack = attackDelay;
                        playerHealth.HealthPercentage -= attackDmg;
                        // ANIMATION attack hit
                        animator.SetTrigger(CConstants.Animator.CrowAttackHit);
                    }
                }
                else
                {
                    state = State.WALK_TO_PLAYER;
                    timer = 5.0f;
                    // ANIMATION WALK
                    animator.SetTrigger(CConstants.Animator.CrowWalk);
                }
            }

            break;

            case State.WALK_TO_PLAYER:
            {
                // Player not specified
                if (playerHealth == null)
                {
                    state = State.RETURN_TO_AIR;
                    return;
                }

                RotateToPlayer();

                Vector3 playerPosition = playerHealth.gameObject.transform.position;
                float playerDistance = (playerPosition - transform.position).magnitude;

                // Follow player
                if (playerDistance < 20.0f && playerPosition.y < transform.position.y + 5.0f)
                {
                    // Move the object
                    transform.position += transform.forward * Time.deltaTime * walkSpeed;
                    timer = 5.0f;

                    if (playerDistance <= 1.5f)
                    {
                        state = State.COMBAT;
                        // ANIMATION STAND
                        animator.SetTrigger(CConstants.Animator.CrowStand);
                    }
                }
                else
                {
                    timer -= Time.deltaTime;
                    // Return back to air
                    if (timer < 0.0)
                    {
                        ReturnToAir();
                    }
                }
            }

            break;

            default:
                break;

        }
    }

    private void ReturnToAir()
    {
        state = State.RETURN_TO_AIR;

        // ANIMATION FLY
        animator.SetTrigger(CConstants.Animator.CrowFly);

        float direction = Random.Range(0.0f, 6.28f);
        float distance = groundAttackDistance;
        waypoint = new Vector3(Mathf.Sin(direction) * distance, 0.0f, Mathf.Cos(direction) * distance);
        waypoint += transform.position;
        waypoint.y = flightHeight;
    }

    private void RotateToPlayer()
    {
        // Get direction to player
        Vector3 playerPosition = playerHealth.gameObject.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);


        // Slowly change direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * angularVelocity * 10.0f);
    }

    private void GenerateWaypoint()
    {
        float x = Random.Range(-waypointAreaRange, waypointAreaRange);
        float y = Random.Range(-waypointAreaRange, waypointAreaRange);
        waypoint = new Vector3(x, y, flightHeight);
    }

    private void FlyToPosition(Vector3 targetPosition, bool stayHigh, float angularSpeedMultiplier)
    {
        // Do not fly to ground, stay in the air
        if (stayHigh)
            targetPosition.y = flightHeight;

        // Get direction to target
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);


        // Slowly change direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * angularVelocity * angularSpeedMultiplier);

        // Move the object
        transform.position += transform.forward * Time.deltaTime * flightSpeed;

    }

    private void FindTarget()
    {
        Object originalObject = targetObject;

        targetObject = null;
        targetPosition = null;

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
                state = State.APPROACHING;
            }
        }
    }
}
