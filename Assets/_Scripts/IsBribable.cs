using UnityEngine;

public class IsBribable : MonoBehaviour
{


    public GuardAI guard; // Reference to the guard
    public Sprite bribeableIcon;
    public Sprite bribedIcon;
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Position above the guard
    public SpriteRenderer iconRenderer;

    private bool lastBribeableState;
    private bool lastFriendlyState;

    void Start()
    {
        if (!iconRenderer) iconRenderer = GetComponent<SpriteRenderer>();
        if (!guard) guard = GetComponentInParent<GuardAI>(); // Optional fallback
    }

    void Update()
    {
        if (!guard || !iconRenderer) return;

        // Follow guard
        transform.position = guard.transform.position + offset;

        // Update icon only if bribe/friendly state changes
        if (guard.enemyData.bribeable != lastBribeableState || guard.enemyData.friendly != lastFriendlyState)
        {
            UpdateIcon();
            lastBribeableState = guard.enemyData.bribeable;
            lastFriendlyState = guard.enemyData.friendly;
        }
    }

    void UpdateIcon()
    {
        if (guard.enemyData.friendly)
        {
            iconRenderer.sprite = bribedIcon;
            iconRenderer.color = Color.green; 
            iconRenderer.enabled = true;
        }
        else if (guard.enemyData.bribeable)
        {
            iconRenderer.sprite = bribeableIcon;
            iconRenderer.color = Color.yellow;
            iconRenderer.enabled = true;
        }
        else
        {
            iconRenderer.sprite = null;
            iconRenderer.enabled = false;
        }
    }

}
