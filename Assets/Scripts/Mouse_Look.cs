using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    public float mouseSensitivity = 400f;

    public Transform playerBody; // Reference to the player's body

    float xRotation = 0f; // Rotation around the x-axis

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Get the mouse input on the x-axis and multiply it by the mouse sensitivity and time 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Get the mouse input on the y-axis and multiply it by the mouse sensitivity and time

        xRotation -= mouseY; // Subtract the mouse input on the y-axis from the xRotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the xRotation between -90 and 90 degrees to prevent the camera from rotating too far up or down

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate the camera on the x-axis by the xRotation
        playerBody.Rotate(Vector3.up * mouseX); // Rotate the player's body on the y-axis by the mouse input on the x-axis 
    }
}
