using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Item/Key")]
public class Key : ItemInfo
{
    [SerializeField] Thief player;
}
