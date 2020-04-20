using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAnimatorHelper : MonoBehaviour
{
    public Collider[] colliders;
    public GameObject self;
    public Rigidbody rb;

    private void BurrowStart()
    {
        rb.useGravity = false;
    }
    private void DisableColliders()
    {
        rb.useGravity = true;
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void DestroySelf()
    {
        if (self != null) Destroy(self);
    }
}
