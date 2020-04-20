using System.Collections.Generic;
using UnityEngine;

public class ShrubInstancer : MonoBehaviour
{
    public int shrubCount = 5;
    public GameObject[] shrubPrefabs;
    public SphereCollider[] areas;
    private void Start()
    {
        var instances = new List<GameObject>();
        for(var i = 0; i < shrubCount; ++i)
        {
            var area = areas[i % areas.Length];
            var shrub = shrubPrefabs[Random.Range(0, shrubPrefabs.Length)];
            var position2D = Random.insideUnitCircle;
            var position = new Vector3(position2D.x, 0f, position2D.y);
            position *= area.radius;
            position += area.transform.position + area.center;
            position = area.ClosestPoint(position);
            instances.Add(Instantiate(shrub, position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), transform));
        }
        foreach (var instance in instances)
        {
            var startPosition = instance.transform.position;
            if (Physics.Raycast(startPosition, Vector3.down, out var info, 50f, -1))
            {
                instance.transform.position = info.point;
            }
        }
    }
}
