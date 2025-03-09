using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void StartGame() // Called when the game starts
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene in the build order
        Debug.Log("Game Started");
    }

    public void QuitGame() // Called when the game quits
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Quit the game
    }
}
