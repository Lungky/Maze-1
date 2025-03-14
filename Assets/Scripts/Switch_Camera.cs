using UnityEngine;

public class Switch_Camera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public int Manager; //public int Manager;

    public Third_Person_Movement thirdPersonMovement; // Reference to the Third_Person_Movement script
    public First_Person_Movement firstPersonMovement; // Reference to the First_Person_Movement script

    // Store all Renderer components in the character and its children.
    private Renderer[] renderers;

    void Start()
    {
        Cam_1(); // Start with TPS mode

        // Gather all Renderer components.
        renderers = GetComponentsInChildren<Renderer>();
        // Optionally start with the character hidden.
        SetRenderersEnabled(true);
    }

    void Update()
    {
        // Listen for the "C" key press.
        if (Input.GetKeyDown(KeyCode.V))
        {
            ManageCamera(); // Call the ManageCamera function
            ToggleRenderers(); // Toggle character visibility when the "V" key is pressed.
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

    // Toggle the enabled state of all renderers.
    void ToggleRenderers()
    {
        // Check the current state using the first renderer (assuming all share the same state).
        bool currentlyEnabled = renderers.Length > 0 && renderers[0].enabled;
        SetRenderersEnabled(!currentlyEnabled);
    }

    // Enable or disable all renderer components.
    void SetRenderersEnabled(bool enabled)
    {
        foreach (Renderer rend in renderers)
        {
            rend.enabled = enabled;
        }
    }
}