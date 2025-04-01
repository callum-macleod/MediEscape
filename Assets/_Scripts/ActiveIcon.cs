using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActiveIcon : MonoBehaviour
{
    public Hotbar hotbar; // Reference to the Hotbar script
    public Transform player; // Reference to the Player's transform
    public Vector3 offset = new Vector3(0, 2f, 0); // Position above the player's head
    public SpriteRenderer iconRenderer; // Sprite Renderer to show the icon above the player
    private ItemInfo lastItem;

    private bool HasItemChanged()
    {
        return hotbar.items[hotbar.selectedIndex] != lastItem;
    }



    private int lastIndex = -1;

    void Start()
    {
        if (!hotbar)
        {
            hotbar = FindObjectOfType<Hotbar>();
        }

        if (!player)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (!hotbar || !player || !iconRenderer) return;

        // Follow the player with offset
        transform.position = player.position + offset;

        // Check if the selected item changed
        if (hotbar.selectedIndex != lastIndex || HasItemChanged())
        {
            UpdateIcon();
            lastIndex = hotbar.selectedIndex;
            lastItem = hotbar.items[hotbar.selectedIndex];
        }
    }

    void UpdateIcon()
    {
        var selectedItem = hotbar.items[hotbar.selectedIndex];
        if (selectedItem != null && selectedItem.itemIcon != null)
        {
            iconRenderer.sprite = selectedItem.itemIcon;
            iconRenderer.enabled = true;
        }
        else
        {
            iconRenderer.sprite = null;
            iconRenderer.enabled = false;
        }
    }

}
