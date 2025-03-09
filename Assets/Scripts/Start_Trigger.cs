using UnityEngine;

public class Start_Trigger : MonoBehaviour
{
    public Timer Timer; // Reference to the Timer script
    public Game_Manager gameManager; // Reference to the Game_Manager script
    void OnTriggerEnter()
    {
        Timer.TimerStart(); // Call the TimerStart method from the Timer script 
        gameManager.BeginLevel(); // Call the BeginLevel method from the Game_Manager script
    }
}
