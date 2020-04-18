using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    public float interpolationSpeed = 1f;
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * interpolationSpeed);
    }
}
