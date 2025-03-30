using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{
    [SerializeField] GameObject LockedSpriteMap;
    [SerializeField] GameObject UnlockedSpriteMap;

    BoxCollider2D _collider;
    BoxCollider2D collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider2D>();
            return _collider;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        LockedSpriteMap.SetActive(false);
        UnlockedSpriteMap.SetActive(true);
        collider.enabled = false;
    }
}
