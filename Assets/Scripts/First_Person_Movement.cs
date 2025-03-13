using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public float speed = 6f; // Movement speed
    
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        float z = Input.GetAxis("Vertical"); // Get the vertical input

        Vector3 move = transform.right * x + transform.forward * z; // Calculate the movement vector by adding the right vector multiplied by x and the forward vector multiplied by z

        controller.Move(move * speed * Time.deltaTime); // Move the player in the direction of the movement vector multiplied by the speed and time
    }
}
