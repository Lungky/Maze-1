using UnityEngine;

public class Finish_Trigger : MonoBehaviour
{
    public Game_Manager gameManager; // Reference to the GameManager script
    public Timer timer; // Reference to the Timer script
    void OnTriggerEnter()
    {
        timer.TimerStop(); // Call the TimerStop method from the Timer script
        gameManager.CompleteLevel(); // Call the CompleteLevel method from the GameManager script

        Cursor.lockState = CursorLockMode.None; // Lock the cursor to the center
        Cursor.visible = true;                   // Hide the cursor
    }
}
