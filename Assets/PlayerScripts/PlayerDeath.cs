using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreen;
    
    void Start()
    {
        // Make sure the death screen is not visible when the game starts
        deathScreen.SetActive(false);
    }
    
    // Call this method when the player dies
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        // Optionally, pause the game
        // Time.timeScale = 0;
    }

    // Example restart method linked to the restart button
    public void RestartGame()
    {
        // Unpause the game if paused
        // Time.timeScale = 1;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
    }
}