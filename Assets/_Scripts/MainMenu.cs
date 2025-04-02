using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Tomm");
    }    


    public void GameQuit()
    {
        Application.Quit();

    }
}
