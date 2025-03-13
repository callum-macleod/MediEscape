using Unity.Cinemachine;
using UnityEngine;

public class mgr : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachine;
    [SerializeField] Transform thiefSpawnPoint;
    [SerializeField] GameObject thiefPrefab;
    GameObject thiefRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thiefRef = Instantiate(thiefPrefab, thiefSpawnPoint.position, Quaternion.identity);
        thiefRef.name = "Thief";
        cinemachine.Follow = thiefRef.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
