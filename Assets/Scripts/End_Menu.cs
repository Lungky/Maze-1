using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Menu : MonoBehaviour
{
    public void RestartGame() // Called when the game restarts
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Load the previous scene in the build order
        Debug.Log("Game Restarted");
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // Load the scene at index 0 in the build order
        Debug.Log("Main Menu");
    }
}
