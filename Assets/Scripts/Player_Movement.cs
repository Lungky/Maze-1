using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 10000f;
    public float sidewaysForce = 10000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     //   rb.AddForce(0, 0, forwardForce * Time.deltaTime); // This line adds a forward force to the player

        if (Input.GetKey("d")) // This line checks if the "d" key is pressed
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0); // This line adds a rightward force to the player
        }

        if (Input.GetKey("a")) // This line checks if the "a" key is pressed
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0); // This line adds a leftward force to the player
        }

        if (Input.GetKey("w")) // This line checks if the "w" key is pressed
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime); // This line adds a forward force to the player
        }

        if (Input.GetKey("s")) // This line checks if the "s" key is pressed
        {
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime); // This line adds a backward force to the player
        }
    }
}
