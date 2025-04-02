using UnityEngine;

public class DeathScreenMgr : MonoBehaviour
{

    [SerializeField] GameObject deathScreenUI;

    public static DeathScreenMgr Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        deathScreenUI.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        deathScreenUI.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
