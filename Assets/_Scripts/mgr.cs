using UnityEngine;

public class mgr : MonoBehaviour
{
    [SerializeField] Transform thiefSpawnPoint;
    [SerializeField] GameObject thiefPrefab;
    GameObject thiefRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thiefRef = Instantiate(thiefPrefab, thiefSpawnPoint.position, Quaternion.identity);
        thiefRef.name = "Thief";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
