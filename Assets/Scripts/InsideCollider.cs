using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InsideCollider : MonoBehaviour
{
    private HashSet<GameObject> objects = new HashSet<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
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
