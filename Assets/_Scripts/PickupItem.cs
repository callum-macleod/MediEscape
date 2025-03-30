using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] ItemInfo itemData;
    [SerializeField] bool canBePickedUp = true;
    [SerializeField] float pickupRange = 1f;

    private Transform player;
    private bool isPlayerNearby = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(player == null || itemData == null) return;
    }

    void Pickup()
    {
        if(!canBePickedUp || itemData == null) return;

        canBePickedUp = false;

        // TODO: Replace with inventory logic

        Destroy(gameObject);
    }

    public void SetItemInfo(ItemInfo newItemData)
    {
        itemData = newItemData;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}