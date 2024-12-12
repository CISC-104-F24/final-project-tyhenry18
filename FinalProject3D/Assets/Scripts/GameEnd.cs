using UnityEngine;
using UnityEngine.SceneManagement; // Required to use SceneManager

public class EndGameTimer : MonoBehaviour
{
    public float timeLimit = 120f; // 2 minutes in seconds

    private float elapsedTime = 0f;

    void Update()
    {
        // Increment elapsed time by the time passed since the last frame
        elapsedTime += Time.deltaTime;

        // Check if the elapsed time exceeds the time limit
        if (elapsedTime >= timeLimit)
        {
            // Load the EndGame scene
            SceneManager.LoadScene("EndScreen");
        }
    }
}
