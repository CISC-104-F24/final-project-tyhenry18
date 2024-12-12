using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class ReturnToStart : MonoBehaviour
{
    // This method will be triggered when the player presses the "Title Screen" button
    public void OnTitleScreenButtonPressed()
    {
        // Load the MainGame scene
        SceneManager.LoadScene("GameStart");
    }
}