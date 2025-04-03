using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class UnlockableDoor : MonoBehaviour
{
    [SerializeField] GameObject[] LockedSpriteMaps;
    [SerializeField] GameObject[] UnlockedSpriteMaps;

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

    Hotbar _hotbar;
    Hotbar hotbar
    {
        get
        {
            if (_hotbar == null)
                _hotbar = GameObject.Find("Hotbar").GetComponent<Hotbar>();
            return _hotbar;
        }
    }

    public void Unlock()
    {
        foreach (GameObject map in LockedSpriteMaps)
            map.SetActive(false);

        foreach (GameObject map in UnlockedSpriteMaps)
            map.SetActive(true);

        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Layers.Player)
        {
            Key key = (Key) hotbar.items.FirstOrDefault(a => a != null && a.GetType() == typeof(Key));
            if (key != null)
            {
                Unlock();
                int idx = hotbar.items.IndexOf(key);
                hotbar.items[idx] = null;
                hotbar.UpdateHotbarUI();
                collision.gameObject.GetComponentInChildren<ActiveIcon>().UpdateIcon();
                AudioMgr.Instance.PlayDoorSound(transform);
            }
        }
    }
}
