using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraXRig;
    public CharacterController controller;
    public float flySpeedMultiplier = 1.5f;
    public float moveSpeed = 5f;
    public float verticalSpeed = 5f;
    public Vector2 rotationSpeed = Vector2.zero;
    public Animator animator;

    private bool isFlying = false;
    private Vector3 movement = Vector3.zero;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private void HandleInputs()
    {
        if (Input.GetButtonDown(CConstants.Input.Fly))
        {
            isFlying = !isFlying;
            animator.SetBool(CConstants.Animator.PlayerIsFlying, isFlying);
        }
        var horizontalMovement = Input.GetAxis(CConstants.Input.HorizontalAxis);
        var verticalMovement = Input.GetAxis(CConstants.Input.VerticalAxis);
        var y = movement.y;
        movement.y = 0;
        movement.x = horizontalMovement;
        movement.z = verticalMovement;
        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }
        movement.y = y;
        xRotation += -Input.GetAxis(CConstants.Input.MouseY) * rotationSpeed.x;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Input.GetAxis(CConstants.Input.MouseX);
    }

    private void MoveAndRotate()
    {
        var topLayerHeight = FlightVariables.instance.topLayerHeight;
        if (isFlying && transform.position.y < topLayerHeight)
        {
            movement.y = verticalSpeed;
        } else if (!controller.isGrounded)
        {
            movement.y += Physics.gravity.y * Time.deltaTime;
        } else
        {
            movement.y = 0;
        }
        if (isFlying)
        {
            movement.x *= flySpeedMultiplier;
            movement.z *= flySpeedMultiplier;
        }
        var scaledMovement = transform.TransformVector(movement) * moveSpeed;
        controller.Move(scaledMovement * Time.deltaTime);
        transform.Rotate(Vector3.up, yRotation * rotationSpeed.y * Time.deltaTime);
        cameraXRig.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if (transform.position.y > topLayerHeight)
        {
            var position = transform.position;
            position.y = topLayerHeight;
            transform.position = position;
        }
        scaledMovement.y = 0f;
        animator.SetFloat(CConstants.Animator.PlayerSpeed, scaledMovement.magnitude);
    }
    private void Update()
    {
        HandleInputs();
        MoveAndRotate();
    }
}
