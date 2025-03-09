using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; // Offset the camera's position on x, y, and z axes

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset; // Transform the camera's position to the player's position with the added offset
    }
}
