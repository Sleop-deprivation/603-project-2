using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// Load the next scene. If the current scene is the last scene, loop to the first scene. 
    /// </summary>
    public void GoToNextScene()
    {
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex >= totalSceneCount - 1) currentSceneIndex = -1;

        SceneManager.LoadScene(++currentSceneIndex);
    }
    public void GoToPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(--currentSceneIndex);
    }

    public void LoadPenultimate()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 2);
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
