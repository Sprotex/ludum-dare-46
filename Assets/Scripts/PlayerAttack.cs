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
    }
}
