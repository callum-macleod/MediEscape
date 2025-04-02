using UnityEngine;

public class GuardStateIcon : MonoBehaviour
{
    public GuardAI guard;  // Link to the guard script
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Position above the guard
    public SpriteRenderer iconRenderer;

    [Header("State Icons")]
    public Sprite patrolIcon;
    public Sprite chaseIcon;
    public Sprite searchIcon;
    public Sprite bribeableIcon;
    public Sprite bribedIcon;

    [Header("State Colors")]
    public Color patrolColor = Color.white;
    public Color chaseColor = Color.red;
    public Color searchColor = Color.yellow;
    public Color bribeableColor = Color.cyan;
    public Color bribedColor = Color.green;

    private GuardState lastState;
    private bool lastBribeable;
    private bool lastFriendly;

    void Start()
    {
        if (!iconRenderer) iconRenderer = GetComponent<SpriteRenderer>();
        if (!guard) guard = GetComponentInParent<GuardAI>();
        UpdateIcon(true);
    }

    void Update()
    {
        if (!guard || !iconRenderer) return;

        transform.position = guard.transform.position + offset;

        if (StateChanged())
        {
            UpdateIcon();
        }
    }

    bool StateChanged()
    {
        return guard.STATE != lastState
            || guard.enemyData.bribeable != lastBribeable
            || guard.enemyData.friendly != lastFriendly;
    }

    void UpdateIcon(bool force = false)
    {
        lastState = guard.STATE;
        lastBribeable = guard.enemyData.bribeable;
        lastFriendly = guard.enemyData.friendly;

        if (guard.enemyData.friendly)
        {
            iconRenderer.sprite = bribedIcon;
            iconRenderer.color = bribedColor;
        }
        else if (guard.enemyData.bribeable)
        {
            iconRenderer.sprite = bribeableIcon;
            iconRenderer.color = bribeableColor;
        }
        else if( !guard.enemyData.friendly && !guard.enemyData.bribeable)
        {
            switch (guard.STATE)
            {
                case GuardState.PATROL:
                    iconRenderer.sprite = patrolIcon;
                    iconRenderer.color = patrolColor;
                    break;
                case GuardState.CHASE:
                    iconRenderer.sprite = chaseIcon;
                    iconRenderer.color = chaseColor;
                    break;
                case GuardState.SEARCH:
                    iconRenderer.sprite = searchIcon;
                    iconRenderer.color = searchColor;
                    break;
                default:
                    iconRenderer.sprite = null;
                    break;
            }
        }

        iconRenderer.enabled = iconRenderer.sprite != null;
    }
}
