using UnityEngine;
using UnityEngine.UI;


public class Hotbar : MonoBehaviour
{
    public Item[] items = new Item[4]; // Holds the items in the hotbar
    public Image[] slotImages;         // item sprite replace hotbar
    public int selectedIndex = 0;

    //scale for selected slots
    public float selectedScale = 1.05f;           
    public float normalScale = 1f;

    public HealthyEntity playerHealth; // Drag player in Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHotbarUI();
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
            if (items[selectedIndex] is HealthPotion hpotion)
            {
                hpotion.Use(playerHealth); 
            }
            else
            {
                items[selectedIndex].Use(); 
            }

            
            
            items[selectedIndex] = null;
            

            UpdateHotbarUI();
        }
    }


    public void UpdateHotbarUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (items[i] != null)
                slotImages[i].sprite = items[i].icon;
            else
                slotImages[i].sprite = null;


            // Scale selected slot
            if (i == selectedIndex)
                slotImages[i].rectTransform.localScale = Vector3.one * selectedScale;
            else
                slotImages[i].rectTransform.localScale = Vector3.one * normalScale;

        }


        
    
}

    public void AddItemToHotbar(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                UpdateHotbarUI();
                return;
            }
        }

        Debug.Log("Hotbar full");
    }
}

