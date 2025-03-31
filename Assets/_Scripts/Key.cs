using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "Key", menuName = "Item/Key")]
public class Key : ItemInfo
{
    [SerializeField] Thief player;
    public void Use()
    {
        //base.Use();

        Collider2D door = Physics2D.OverlapCircle(player.transform.position, 2, 1 << (int)Layers.Door);
        if (door.TryGetComponent<UnlockableDoor>(out UnlockableDoor doorScript))
            doorScript.Unlock();
    }
}
