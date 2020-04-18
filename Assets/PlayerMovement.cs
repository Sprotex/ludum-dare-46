using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraXRig;

    public float moveSpeed = 5f;
    public Vector2 rotationSpeed = Vector2.zero;

    private bool isFlying = false;
    private Vector3 movement = Vector3.zero;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private void HandleInputs()
    {
        if (Input.GetButtonDown("Fly"))
        {
            isFlying = !isFlying;
        }
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        movement.x = horizontalMovement;
        movement.z = verticalMovement;
        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }
        xRotation += -Input.GetAxis("Mouse Y") * rotationSpeed.x;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Input.GetAxis("Mouse X");
    }

    private void MoveAndRotate()
    {
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, yRotation * rotationSpeed.y * Time.deltaTime);
        cameraXRig.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Update()
    {
        HandleInputs();
        MoveAndRotate();
    }
}
