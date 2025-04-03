using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private ItemInfo itemData;            // The item info
    [SerializeField] private bool canBePickedUp = true;
    private bool isCollected = false;                  // Prevent multiple pickups

    private void Start()
    {
        // Ensure the collider is set to trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected || !canBePickedUp || itemData == null) return;

        // Check if the player collided
        if (collision.CompareTag("Player"))
        {
            // Try to add the item to the hotbar
            Hotbar hotbar = FindFirstObjectByType<Hotbar>();
            
            if (hotbar != null){
                if(hotbar.HasFreeSlot()){
                    Pickup(hotbar, collision.gameObject);
                }
                else
                {
                    Debug.Log("Hotbar full");
                }
            }
            else
                Debug.Log("Hotbar missing!");
        }
    }

    private void Pickup(Hotbar hotbar, GameObject player)
    {
        if (!canBePickedUp || itemData == null) return;

        canBePickedUp = false;
        isCollected = true;

        Debug.Log($"Hotbar Reference: {hotbar}");  // Check if the hotbar is detected

        AudioMgr.Instance.PLayPickupItemSound(transform);
        if (hotbar != null)
        {
            Debug.Log($"Attempting to add {itemData.name} to Hotbar");
            hotbar.AddItemToHotbar(itemData);  // Add the item
            player.GetComponentInChildren<ActiveIcon>().UpdateIcon();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Hotbar not found!");
        }
    }

    // Set the item's data when instantiated
    public void SetItemInfo(ItemInfo newItemData)
    {
        itemData = newItemData;
    }
}