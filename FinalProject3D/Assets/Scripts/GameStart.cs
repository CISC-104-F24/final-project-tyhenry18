using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class GameStart : MonoBehaviour
{
    // This method will be triggered when the player presses the "Start" button
    public void OnStartButtonPressed()
    {
        // Load the MainGame scene
        SceneManager.LoadScene("MainGame");
    }
}
