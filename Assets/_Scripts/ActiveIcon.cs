using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveIcon : MonoBehaviour
{
    public Hotbar hotbar;
    public Transform player;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public SpriteRenderer iconRenderer;

    public Sprite timerIcon; // Assign your circle/timer sprite here

    private ItemInfo lastItem;
    private int lastIndex = -1;
    private Coroutine iconChangeRoutine;

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

        if (hotbar)
               hotbar.OnItemUsed += HandleItemUsed;
        
            
    }
    void HandleItemUsed(ItemInfo usedItem)
    {
        if (iconChangeRoutine != null)
            StopCoroutine(iconChangeRoutine);

        iconChangeRoutine = StartCoroutine(ShowTransitionIcon());
    }

    void Update()
    {

        transform.position = player.position + offset;

       
    }

    private bool HasItemChanged()
    {
        return hotbar.items[hotbar.selectedIndex] != lastItem;
    }

    IEnumerator ShowTransitionIcon()
    {
        if (timerIcon != null)
        {
            iconRenderer.sprite = timerIcon;
            iconRenderer.enabled = true;
        }

        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            // Spin the icon around its Z axis
            transform.Rotate(Vector3.forward * 360 * Time.deltaTime); // 360 degrees per second
            time += Time.deltaTime;
            yield return null;
        }

        // Reset rotation (optional)
        transform.rotation = Quaternion.identity;

        // Swap back to actual icon
        UpdateIcon();
        iconChangeRoutine = null;
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
