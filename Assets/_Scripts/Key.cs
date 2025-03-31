using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Item/Key")]
public class Key : ItemInfo
{
    [SerializeField] Thief player;
    public override void Use()
    {
        base.Use();

        Collider2D door = Physics2D.OverlapCircle(player.transform.position, 100, 1 << (int)Layers.Door);
        if (door.TryGetComponent<UnlockableDoor>(out UnlockableDoor doorScript))
            doorScript.Unlock();
    }
}
