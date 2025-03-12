using UnityEngine;

public class Switch_Camera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public int Manager; //public int Manager;

    public Third_Person_Movement thirdPersonMovement; // Reference to the Third_Person_Movement script
    public First_Person_Movement firstPersonMovement; // Reference to the First_Person_Movement script

    void Start()
    {
        Cam_1(); // Start with TPS mode
    }

    void Update()
    {
        // Listen for the "C" key press.
        if (Input.GetKeyDown(KeyCode.C))
        {
            ManageCamera(); // Call the ManageCamera function
        }
    }

    public void ManageCamera()
    {
        if (Manager == 0)
        {
            Cam_2(); // Switch to FPS mode
            Manager = 1;
        }
        else
        {
            Cam_1(); // Switch to TPS mode
            Manager = 0;
        }
    }
    void Cam_1() // TPS mode
    {
        thirdPersonMovement.enabled = true; // Enable the Third_Person_Movement script
        firstPersonMovement.enabled = false; // Disable the First_Person_Movement script
        Camera_1.SetActive(true); // Enable the TPS camera
        Camera_2.SetActive(false); // Disable the FPS camera
    }
    void Cam_2() // FPS mode
    {
        thirdPersonMovement.enabled = false; // Disable the Third_Person_Movement script
        firstPersonMovement.enabled = true; // Enable the First_Person_Movement script
        Camera_1.SetActive(false); // Disable the TPS camera
        Camera_2.SetActive(true); // Enable the FPS camera
    }
}