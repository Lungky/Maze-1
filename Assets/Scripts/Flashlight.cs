using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    private bool FlashlightActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Toggle flashlight on/off
        {
            FlashlightActive = !FlashlightActive; // Toggle the flashlight state
            FlashlightLight.gameObject.SetActive(FlashlightActive); // Enable/disable the flashlight light
        }
    }
}
