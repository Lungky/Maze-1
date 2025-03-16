using UnityEngine;

public class Minimap_Icon : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform component

    private void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
    }
}