using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public float speed = 6f; // Movement speed
    public ParticleSystem dust; // Reference to the ParticleSystem component

    public SpriteRenderer spriteRenderer;

    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        float z = Input.GetAxis("Vertical");   // Get the vertical input

        Vector3 move = transform.right * x + transform.forward * z; // Calculate the movement vector

        // Always move the character
        controller.Move(move * speed * Time.deltaTime);

        // If the character is moving (magnitude > threshold), play dust; otherwise stop it.
        if (move.magnitude > 0.1f)
        {
            CreateDust();
        }
        else if (dust.isPlaying)
        {
            dust.Stop();
        }

        // Ensure the spriteRenderer is not null and enable it
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    void CreateDust() // This method is used to create dust particles
    {
        // Check if dust is not already playing to avoid restarting it repeatedly.
        if (!dust.isPlaying)
        {
            dust.Play();
        }
    }
}
