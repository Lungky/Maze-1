using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    bool gameHasEnded = false; // Boolean to check if the game has ended
    public float restartDelay = 1f; // Delay before the game restarts
    public GameObject completeLevelUI; // Reference to the level complete UI

    public void BeginLevel() // Called when the level starts
    {
        Debug.Log("Level Started");
    }

    public void CompleteLevel() // Called when the level is completed
    {
        Debug.Log("Level Complete");
        //completeLevelUI.SetActive(true); // Activate the level complete UI
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene in the build order
    }

    public void EndGame() // Called when the game ends
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay); // Restart the game after a delay
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the game
    }
}
