using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float explosionLength = 1f;
    public float explosionMaxScale = 2f;

    private float startTime = 0f;
    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        var t = Mathf.InverseLerp(startTime, startTime + explosionLength, Time.time);
        t = 1 - t;
        t = t * t * t;
        t = 1 - t;
        var s = Mathf.Lerp(1f, explosionMaxScale, t);
        transform.localScale = Vector3.one * s;
        if (Time.time > startTime + explosionLength)
        {
            Destroy(gameObject);
        }
    }
}
