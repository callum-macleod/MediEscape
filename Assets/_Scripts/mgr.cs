using Unity.Cinemachine;
using UnityEngine;

public class mgr : MonoBehaviour
{
    [Header("Player")]
    // [SerializeField] CinemachineCamera cinemachine;
    [SerializeField] Transform thiefSpawnPoint;
    [SerializeField] GameObject thiefPrefab;
    GameObject thiefRef;

    [Header("Guards")]
    [SerializeField] Transform guardSpawnPoint;
    [SerializeField] GameObject guardPrefab;
    GameObject guardRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thiefRef = Instantiate(thiefPrefab, thiefSpawnPoint.position, Quaternion.identity);
        thiefRef.name = "Thief";
        // cinemachine.Follow = thiefRef.transform;

        guardRef = Instantiate(guardPrefab, guardSpawnPoint.position, Quaternion.identity);
        guardRef.name = "Guard1";
        GuardAI guardAI = GetComponent<GuardAI>();
        guardAI.target = thiefRef.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
