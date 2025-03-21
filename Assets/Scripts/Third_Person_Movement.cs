using UnityEngine;

public class Third_Person_Movement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public Animator animator;              // Reference to the Animator component
    public Transform cam;                  // Camera transform

    public float speed = 3f;               // Movement speed
    public float turnSmoothTime = 0.1f;      // Smooth time for turning

    float turnSmoothVelocity;              // Velocity for turning

    public ParticleSystem dust;            // Reference to the ParticleSystem component

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center
        Cursor.visible = false;                   // Hide the cursor
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        float vertical = Input.GetAxisRaw("Vertical");       // Get vertical input

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Calculate direction

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // Trigger the idle -> moving transition in the animator.
            animator.SetBool("IsMoving", true);

            // Play dust particles only if not already playing.
            CreateDust();
        }
        else
        {
            // Trigger the moving -> idle transition in the animator.
            animator.SetBool("IsMoving", false);

            // Stop dust particles if they are playing.
            if (dust.isPlaying)
            {
                dust.Stop();
            }
        }
    }

    // This method plays dust particles if they are not already playing.
    void CreateDust()
    {
        if (!dust.isPlaying)
        {
            dust.Play();
        }
    }
}
