using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Item/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [Header("Item Details")]
    public string itemName;
    public string description;
    public Sprite itemIcon;
    public int maxStack = 1;

    [Header("Item Properties")]
    public ItemType itemType;
    public int value;
    public GameObject itemPrefab;

    public enum ItemType
    {
        Consumable,
        Inventory,
        QuestItem
    }
}
