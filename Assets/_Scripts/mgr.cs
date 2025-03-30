using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] Transform[] guardSpawnPoints;
    [SerializeField] GameObject soldierPrefab;
    [SerializeField] GameObject knightPrefab;
    List<GameObject> guardRefs = new List<GameObject>();
    [SerializeField] EnemyInfo soldierInfo;
    [SerializeField] EnemyInfo knightInfo;

    [Header("Camera")]
    [SerializeField] CinemachineCamera cinemachineCamera;


    [SerializeField] bool debugGuards = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!debugGuards){    
            thiefRef = Instantiate(thiefPrefab, thiefSpawnPoint.position, Quaternion.identity);
            thiefRef.name = "Thief";
        }
        // cinemachine.Follow = thiefRef.transform;

        int soldier_count = 0;
        int knight_count = 0;
        foreach(Transform sp in guardSpawnPoints){
            Debug.LogWarning($"SpawnPoint {sp.name}");
            GameObject guard = null;
            if(sp.CompareTag("Knight")){
                guard = Instantiate(knightPrefab, sp.position, Quaternion.identity);
                guard.name = $"Knight_{knight_count}";
                knight_count++;
                Debug.LogWarning($"Spawned {guard.name}");
                guardRefs.Add(guard);
                GuardAI guardAI = guard.GetComponent<GuardAI>();
                guardAI.target = thiefRef.transform;
                guardAI.enemyData = knightInfo;
                guardAI.patrolAreaCenter = sp.transform;
            }else if(sp.CompareTag("Soldier")){
                guard = Instantiate(soldierPrefab, sp.position, Quaternion.identity);
                guard.name = $"Soldier_{soldier_count}";
                soldier_count++;
                Debug.LogWarning($"Spawned {guard.name}");
                guardRefs.Add(guard);
                GuardAI guardAI = guard.GetComponent<GuardAI>();
                guardAI.target = thiefRef.transform;
                guardAI.enemyData = soldierInfo;
                guardAI.patrolAreaCenter = sp.transform;
            }else{
                Debug.LogError("NO TAG");
            }

            if(guard != null){
                guardRefs.Add(guard);

                GuardAI guardAI = guard.GetComponent<GuardAI>();
                
                guardAI.target = thiefRef.transform;
            }
        }

        cinemachineCamera.Follow = thiefRef.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
