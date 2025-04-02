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
    [SerializeField] List<Transform> guardSpawnPoints = new List<Transform>();
    [SerializeField] GameObject soldierPrefab;
    [SerializeField] GameObject knightPrefab;
    List<GameObject> guardRefs = new List<GameObject>();
    [SerializeField] EnemyInfo soldierInfo;
    [SerializeField] EnemyInfo knightInfo;
    [SerializeField] private List<ItemInfo> items = new List<ItemInfo>(1);
    [SerializeField] private Key keyItem;
    private List<bool> bribeable = new List<bool>() {true, false};

    [Header("Camera")]
    [SerializeField] CinemachineCamera cinemachineCamera;


    [SerializeField] bool debugGuards = false;
    [SerializeField] HealthMgr healthbar;
    [SerializeField] Hotbar hotbar;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!debugGuards){    
            thiefRef = Instantiate(thiefPrefab, thiefSpawnPoint.position, Quaternion.identity);
            thiefRef.name = "Thief";
            healthbar.ConnectPlayer(thiefRef.GetComponent<HealthyEntity>());
            hotbar.ConnectPlayer(thiefRef.GetComponent<HealthyEntity>());
        }
        // cinemachine.Follow = thiefRef.transform;

        List<Transform> shuffledSpawnPoints = guardSpawnPoints.OrderBy(x => Random.value).ToList();
        List<ItemInfo> shuffledItems = items.OrderBy(x => Random.value).ToList();

        SpawnGuard(shuffledSpawnPoints[0], keyItem);

        foreach(Transform sp in shuffledSpawnPoints.Skip(1)){
            int guardRefsCount = guardRefs.Count - 1;
            int idx = guardRefsCount % shuffledItems.Count;

            SpawnGuard(sp, shuffledItems[idx]);
                
        }

        cinemachineCamera.Follow = thiefRef.transform;
    }

    void SpawnGuard(Transform spawnPoint, ItemInfo item)
    {
        GameObject guard = null;
        EnemyInfo enemyData = null;
        bool bribeFlag = bribeable[guardRefs.Count % 2];
        
        if(spawnPoint.CompareTag("Knight")){
            guard = Instantiate(knightPrefab, spawnPoint.position, Quaternion.identity);
            enemyData = Instantiate(knightInfo);
        }else if(spawnPoint.CompareTag("Soldier")){
            guard = Instantiate(soldierPrefab, spawnPoint.position, Quaternion.identity);
            enemyData = Instantiate(soldierInfo);
            enemyData.bribeable = bribeFlag;
        }else{
            Debug.LogError("NO TAG");
            return;
        }

        enemyData.itemHeld = item;
        guardRefs.Add(guard);
        guard.name = $"{spawnPoint.tag}_{guardRefs.Count}";

        Debug.LogWarning($"Spawned {guard.name} with {item?.itemType}");

        GuardAI guardAI = guard.GetComponent<GuardAI>();
        guardAI.target = thiefRef.transform;
        guardAI.enemyData = enemyData;
        guardAI.patrolAreaCenter = spawnPoint;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
