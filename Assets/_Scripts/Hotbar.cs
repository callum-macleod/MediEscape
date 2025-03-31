using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hotbar : MonoBehaviour
{
    public List<ItemInfo> items = new List<ItemInfo>(4); // Holds the items in the hotbar
    public Image[] slotImages; //hotbar image
    public Image[] slotIcons; // placeholder for items
    public int selectedIndex = 0;

    //scale for selected slots
    public float selectedScale = 1.05f;           
    public float normalScale = 1f;

    public HealthyEntity playerHealth; // Drag player in Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthyEntity>();
        UpdateHotbarUI();
    }

    public void ConnectPlayer(HealthyEntity player)
    {
        playerHealth = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSelectedItem();
        }

        //number keys to change item selected
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelesctSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelesctSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelesctSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelesctSlot(3);
    }

    public void SelesctSlot(int index)
    {
        selectedIndex = index;
        UpdateHotbarUI();
    }

    public void UseSelectedItem()
    {
        if (items[selectedIndex] != null && items[selectedIndex].isUsable)
        {
            //checks if item is healthpotion
            if (items[selectedIndex].itemType == ItemInfo.ItemType.Consumable)
            {
                items[selectedIndex].Use(playerHealth); 
            }

            items[selectedIndex] = null;
            UpdateHotbarUI();
        }
    }

    public void GiveItem(int idx)
    {
        items[idx] = null;
        UpdateHotbarUI();
    }


    public void UpdateHotbarUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (items[i] != null)
            {
                slotIcons[i].sprite = items[i].itemIcon;
                slotIcons[i].enabled = true;
            }
            else
            {
                slotIcons[i].sprite = null;
                slotIcons[i].enabled = false; // Hide if no item
            }

            //if (items[i] != null)
            //    slotImages[i].sprite = items[i].itemIcon;
            //else
            //    slotImages[i].sprite = null;


            // Scale selected slot
            if (i == selectedIndex)
                slotImages[i].rectTransform.localScale = Vector3.one * selectedScale;
            else
                slotImages[i].rectTransform.localScale = Vector3.one * normalScale;

        }

        // Debug.Log("Updated Hotbar");
    }

    public void AddItemToHotbar(ItemInfo item)
    {
        if (HasFreeSlot())
        {
            // Find the first available slot
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    Debug.Log($"Added {item.name} to hotbar at slot {i}");  // Confirm addition
                    UpdateHotbarUI();
                    return;  // Exit as the item has been added
                }
            }
        }
        else
        {
            Debug.Log("Hotbar full");
        }
    }

    public bool HasFreeSlot()
    {
        foreach (var item in items)
        {
            if (item == null) return true;  // Found a free slot
        }
        return false;  // No free slot
    }
}

