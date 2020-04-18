using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public LayerMask fleeMask;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 5f;
    public float accelerationMultiplier = 1f;

    private HashSet<Transform> closeEnemies = new HashSet<Transform>();
    private float currentSpeed = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if ((fleeMask.value & (1 << other.gameObject.layer)) > 0)
        {
            closeEnemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (closeEnemies.Contains(other.transform))
        {
            closeEnemies.Remove(other.transform);
        }
    }

    private void Update()
    {
        var interpolatedTime = Time.deltaTime * accelerationMultiplier;
        if (closeEnemies.Count > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, interpolatedTime);
        } else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, interpolatedTime);
        }
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime);
    }
}
