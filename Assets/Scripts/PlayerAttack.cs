using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public InsideCollider inside;
    public FoodStorage storage;
    public Animator animator;
    public InsideCollider scanner;
    public float[] punchDamagesPercent;

    public void Attack(int count)
    {
        var objectsInRange = scanner.GetObjectsInside();
        foreach (var thing in objectsInRange)
        {
            var health = thing.GetComponent<CrowHealth>();
            if (health != null)
            {
                health.HealthPercentage -= punchDamagesPercent[count];
                break;
            }
        }
    }

    public void Pickup()
    {
        var objects = inside.GetObjectsInside();
        foreach (var thing in objects)
        {
            if (thing != null)
            {
                var foodRef = thing.GetComponent<FoodReference>();
                if (foodRef != null)
                {
                    var food = foodRef.food;
                    var destroy = foodRef.destroy;
                    storage.AddFood(food);
                    inside.Remove(thing);
                    Destroy(destroy);
                    break;
                }
            }
        }
    }

    private void InputAnimation(string input, string triggerName)
    {
        if (Input.GetButton(input))
        {
            animator.SetTrigger(triggerName);
        }
        if (Input.GetButtonUp(input))
        {
            animator.ResetTrigger(triggerName);
        }
    }
    private void Update()
    {
        InputAnimation(CConstants.Input.Pickup, CConstants.Animator.PlayerPickup);
        InputAnimation(CConstants.Input.Attack, CConstants.Animator.PlayerAttack);
    }
}
