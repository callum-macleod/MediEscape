using UnityEngine;
using UnityEngine.UI;

public class LetterMgr : MonoBehaviour
{

    [SerializeField] GameObject letter;
    [SerializeField] GameObject Tips;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        letter.SetActive(true);
        Tips.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && letter.activeSelf)
        {
            ToggleLetter();
        }else if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTips();
        }
    }

    void ToggleTips()
    {
        if (Tips.activeSelf)
        {
            Tips.SetActive(false);
        }
        else if (!Tips.activeSelf)
        {
            Tips.SetActive(true);
        }
    }
    void ToggleLetter()
    {
        letter.SetActive(false);
        ToggleTips();
    }
}
