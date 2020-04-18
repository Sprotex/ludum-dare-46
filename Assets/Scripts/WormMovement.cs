using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public Collider coll;
    public LayerMask fleeMask;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
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

    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
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
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, interpolatedTime);
            if (Time.time - safeTime > timeUntilBurrowingStarts)
            {
                coll.enabled = false;
                StartCoroutine(DestroyAfterSeconds(1f));
            }
        } else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, interpolatedTime);
            safeTime = Time.time;
        }
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime);
    }
}
