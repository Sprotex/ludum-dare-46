using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public Animator animator;
    public LayerMask fleeMask;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 2f;
    public float rotationSpeed = 5f;
    public float accelerationMultiplier = 1f;
    public float timeUntilBurrowingStarts = 2.5f;

    private HashSet<Transform> closeEnemies = new HashSet<Transform>();
    private float currentSpeed = 0f;
    private float safeTime = -500f;

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
        if (transform.position.y < -100f)
        {
            Destroy(gameObject);
        }
        var interpolatedTime = Time.deltaTime * accelerationMultiplier;
        if (closeEnemies.Count > 0)
        {
            animator.SetFloat("Speed", runSpeedMultiplier);
            currentSpeed = Mathf.Lerp(currentSpeed, currentSpeed * runSpeedMultiplier, interpolatedTime);
            if (Time.time - safeTime > timeUntilBurrowingStarts)
            {
                animator.SetTrigger("Burrow");
            }
        } else
        {
            animator.SetFloat("Speed", 1f);
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, interpolatedTime);
            safeTime = Time.time;
        }
        rb.velocity = transform.forward * currentSpeed * Time.deltaTime;
    }
}
