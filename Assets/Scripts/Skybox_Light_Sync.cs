using UnityEngine;

public class SkyboxLightSync : MonoBehaviour
{
    // Reference to your day/night directional light.
    public Light dayNightLight;

    // Base exposure value for the skybox (set this to your desired baseline)
    public float baseExposure = 1f;

    // Optional multiplier for fine tuning the effect.
    public float intensityMultiplier = 1f;

    void Update()
    {
        // Calculate a new exposure based on the light's intensity.
        float newExposure = baseExposure + dayNightLight.intensity * intensityMultiplier;

        // Apply the new exposure to the skybox material.
        RenderSettings.skybox.SetFloat("_Exposure", newExposure);

        // (Optional) Update the environment lighting immediately.
        DynamicGI.UpdateEnvironment();
    }
}