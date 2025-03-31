using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] private CivilianInfo civilianInfo;
    private ItemInfo itemHeld;
    private Hotbar hotbar;

    private bool playerNearby;
    private GameObject player;

    void Start()
    {
        civilianInfo = Instantiate(civilianInfo);
        hotbar = FindFirstObjectByType<Hotbar>();
    }

    void Update()
    {
        itemHeld = civilianInfo.tradable;
        if(playerNearby && Input.GetKeyDown(KeyCode.T)){
            Trade();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            playerNearby = true;
            player = collision.gameObject;
            Debug.Log($"{player.name} is near {gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            playerNearby = false;
            player = null;
            Debug.Log($"Player no longer near {gameObject.name}");
        }
    }

    bool Trade()
    {
        bool tradeable = false;
        int idx = 0;
        Debug.Log($"Hotbar: {hotbar}, Hotbar Items: {hotbar.items}");
        foreach(ItemInfo item in hotbar.items){
            if(item == null){
                idx++;
                continue;
            }
            
            if(item.itemType == ItemInfo.ItemType.Money){
                tradeable = true;
                hotbar.GiveItem(idx);
                SwapItems(item);
                return true;
            }

            idx++;
        }

        if(!tradeable) Debug.Log("You are Poor... Feck off");
        return false;
    }

    private void SwapItems(ItemInfo item)
    {
        if(itemHeld != null){
            hotbar.AddItemToHotbar(itemHeld);
            civilianInfo.tradable = item;
        }
    }
}
