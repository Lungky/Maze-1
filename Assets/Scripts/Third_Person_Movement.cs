using UnityEngine;

public class Third_Person_Movement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam; // Camera transform

    public float speed = 3f; // Movement speed

    public float turnSmoothTime = 0.1f; // Smooth time for turning

    float turnSmoothVelocity; // Velocity for turning

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Get the horizontal input
        float vertical = Input.GetAxisRaw("Vertical"); // Get the vertical input
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Normalize the direction vector so that if the player is moving diagonally, the speed is the same as moving in a straight line

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // Get the angle in degrees
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // Smooth the angle
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // Rotate the player to the target angle
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // Move the player in the direction of the target angle
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}