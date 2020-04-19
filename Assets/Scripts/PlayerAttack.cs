using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public InsideCollider inside;
    public FoodStorage storage;
    private void Update()
    {
        if (Input.GetButton(CConstants.Input.Attack))
        {
            var objects = inside.GetObjectsInside();
            foreach (var thing in objects)
            {
                var food = thing.GetComponent<Food>();
                if (food != null)
                {
                    storage.AddFood(food);
                    inside.Remove(thing);
                    Destroy(thing);
                    break;
                }
            }
        }
    }
}
