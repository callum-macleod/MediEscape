using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}
