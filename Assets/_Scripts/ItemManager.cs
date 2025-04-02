using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Other Items")]
    [SerializeField] public List<Transform> itemSpawnPoints = new List<Transform>();
    [SerializeField] private List<ItemInfo> items = new List<ItemInfo>();
    List<GameObject> itemRefs = new List<GameObject>();
    
    [Header("Quest Items - Town")]
    [SerializeField] private List<Transform> townKeySpawn = new List<Transform>();
    [SerializeField] public ItemInfo keyItem;
    List<GameObject> keyRefs = new List<GameObject>();

    [Header("Quest Items - Prison")]
    [SerializeField] private Transform initKeySpawn;
    [SerializeField] private List<Transform> prisonKeySpawn = new List<Transform>();


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Transform> shuffledSpawnPoints = itemSpawnPoints.OrderBy(x => Random.value).ToList();
        List<ItemInfo> shuffledItems = items.OrderBy(x => Random.value).ToList();

        foreach(Transform sp in shuffledSpawnPoints){
            int idx = itemRefs.Count % shuffledItems.Count;

            SpawnItem(sp, shuffledItems[idx]);
        }

        List<Transform> shuffledTownKeySpawnPoints = townKeySpawn.OrderBy(x => Random.value).ToList();
        List<Transform> shuffledPrisKeySpawnPoints = prisonKeySpawn.OrderBy(x => Random.value).ToList();

        SpawnKey(initKeySpawn, keyItem);
        SpawnKey(shuffledPrisKeySpawnPoints[0], keyItem);

        for(int i=0 ; i < 2 ; i++){
            SpawnKey(shuffledTownKeySpawnPoints[i], keyItem);
        }
    }

    void SpawnItem(Transform spawnPoint, ItemInfo item)
    {
        GameObject itemObj = Instantiate(item.itemPrefab, spawnPoint.position, Quaternion.identity);
        ItemInfo itemClone = Instantiate(item);
        PickupItem pickup = itemObj.AddComponent<PickupItem>();
        if(pickup != null)
            pickup.SetItemInfo(itemClone);
        Collider2D coll = itemObj.GetComponent<Collider2D>();
        coll.isTrigger = true;

        itemRefs.Add(itemObj);
        itemObj.name = $"{item.name}_{itemRefs.Count}";
    }

    void SpawnKey(Transform spawnPoint, ItemInfo key)
    {
        GameObject keyObj = Instantiate(key.itemPrefab, spawnPoint.position, Quaternion.identity);
        ItemInfo keyClone = Instantiate(key);
        PickupItem pickup = keyObj.AddComponent<PickupItem>();
        if(pickup != null)
            pickup.SetItemInfo(keyClone);
        Collider2D coll = keyObj.GetComponent<Collider2D>();
        coll.isTrigger = true;

        keyRefs.Add(keyObj);
        keyObj.name = $"{key.name}_{itemRefs.Count}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
