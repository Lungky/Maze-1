using UnityEngine;

public class Switch_Camera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject Camera_Map;
    public int Manager; // 0 = TPS mode, 1 = FPS mode

    public Third_Person_Movement thirdPersonMovement; // Reference to the Third_Person_Movement script
    public First_Person_Movement firstPersonMovement;   // Reference to the First_Person_Movement script

    // Store all Renderer components in the character and its children.
    private Renderer[] renderers;

    void Start()
    {
        Cam_1(); // Start with TPS mode
        Camera_Map.SetActive(false); // Disable the Map camera

        // Gather all Renderer components.
        renderers = GetComponentsInChildren<Renderer>();
        // Optionally start with the character visible.
        SetRenderersEnabled(true);
    }

    void Update()
    {
        // Listen for the "V" key press only if the map is not active.
        if (!Camera_Map.activeSelf && Input.GetKeyDown(KeyCode.V))
        {
            ManageCamera();   // Toggle between TPS and FPS modes
            ToggleRenderers(); // Toggle character visibility
        }

        // Listen for the "M" key press to toggle the map.
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Camera_Map.activeSelf)
            {
                // If map is active, disable it and revert to the previous camera mode.
                Camera_Map.SetActive(false);
                if (Manager == 0)
                {
                    Cam_1();
                }
                else
                {
                    Cam_2();
                }
            }
            else
            {
                // Activate map mode.
                Cam_Map();
            }
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
        thirdPersonMovement.enabled = true;  // Enable TPS movement
        firstPersonMovement.enabled = false;   // Disable FPS movement
        Camera_1.SetActive(true);              // Enable TPS camera
        Camera_2.SetActive(false);             // Disable FPS camera
    }

    void Cam_2() // FPS mode
    {
        thirdPersonMovement.enabled = false;   // Disable TPS movement
        firstPersonMovement.enabled = true;      // Enable FPS movement
        Camera_1.SetActive(false);               // Disable TPS camera
        Camera_2.SetActive(true);                // Enable FPS camera
    }

    void Cam_Map()
    {
        // Disable both movement scripts.
        thirdPersonMovement.enabled = false;
        firstPersonMovement.enabled = false;
        // Disable TPS and FPS cameras.
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        // Enable the Map camera.
        Camera_Map.SetActive(true);
    }

    // Toggle the enabled state of all renderers.
    void ToggleRenderers()
    {
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