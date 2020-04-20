using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Transform rotationTarget;
    public float interpolationSpeed = 1f;
    
    private void LateUpdate()
    {
        var interpolatedTime = Time.deltaTime * interpolationSpeed;
        transform.position = Vector3.Lerp(transform.position, target.position, interpolatedTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget.rotation, interpolatedTime);
    }
}
