using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public float speed = 6f; // Movement speed
    public ParticleSystem dust; // Reference to the ParticleSystem component

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        float z = Input.GetAxis("Vertical"); // Get the vertical input

        Vector3 move = transform.right * x + transform.forward * z; // Calculate the movement vector

        controller.Move(move * speed * Time.deltaTime); // Move the player

        CreateDust(); // Create dust particles
    }

    void CreateDust() // This method is used to create dust particles
    {
        dust.Play(); // Play the dust particles
    }
}
