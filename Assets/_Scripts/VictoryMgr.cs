using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMgr : MonoBehaviour
{

    private GameObject player;
    [SerializeField] GameObject victoryscreenUI;


    public static VictoryMgr Instance { get; private set; }

    //void Awake()
    //{
    //    if (Instance == null)
    //        Instance = this;
    //    else
    //        victoryscreenUI.SetActive(false);

    //    //victoryscreenUI.SetActive(false);
    //}

    public void ShowVictoryScreen()
    {
        victoryscreenUI.SetActive(true);
        Time.timeScale = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        victoryscreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            if (collision.CompareTag("Player"))
            {
                ShowVictoryScreen();
            }
        }
    }

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

