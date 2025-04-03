using UnityEngine;
using UnityEngine.UI;

public class MapMgr : MonoBehaviour
{

    [SerializeField] GameObject map;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) )
        {
            Togglemap();
        }
        
    }

 
    void Togglemap()
    {

        if (map.activeSelf)
        {
            map.SetActive(false);
        }
        else if (!map.activeSelf)
        {
            map.SetActive(true);
        }
        //map.SetActive(false);
        //ToggleTips();
    }
}
