using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InsideCollider : MonoBehaviour
{
    public LayerMask mask;
    private HashSet<GameObject> objects = new HashSet<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (mask.value & (1 << other.gameObject.layer)) > 0)
        {
            objects.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Remove(other.gameObject);
    }
    public void Remove(GameObject other)
    {
        if (objects.Contains(other))
        {
            objects.Remove(other);
        }
    }
    public List<GameObject> GetObjectsInside()
    {
        return objects.ToList();
    }
}
