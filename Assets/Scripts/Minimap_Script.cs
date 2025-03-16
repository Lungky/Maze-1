using UnityEngine;

public class Minimap_Script : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform component

    private void LateUpdate()
    {
        // Follow the player's position (x and z), but keep the camera's y position.
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // Set a fixed rotation (90° downwards) so the camera doesn't follow the player's rotation.
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
