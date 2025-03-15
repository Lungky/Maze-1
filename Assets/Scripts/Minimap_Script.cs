using UnityEngine;

public class Minimap_Script : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate() // LateUpdate is called after Update each frame and is used to update the minimap camera's position and rotation
    {
        Vector3 newPosition = player.position; // Get the player's position
        newPosition.y = transform.position.y; // Set the y position of the minimap camera to the y position of the player
        transform.position = newPosition; // Set the position of the minimap camera to the new position
        // transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // Set the rotation of the minimap camera to the player's rotation
    }
}
