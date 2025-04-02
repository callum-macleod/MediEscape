using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }    


    public void GameQuit()
    {
        Application.Quit();

    }
}
