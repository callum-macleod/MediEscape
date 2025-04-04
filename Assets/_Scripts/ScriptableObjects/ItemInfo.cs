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
    public bool isUsable;

    public enum ItemType
    {
        Consumable,
        Money,
        QuestItem
    }

    public virtual void Use()
    {
        Debug.Log("Using item: " + itemName);
    }
}
