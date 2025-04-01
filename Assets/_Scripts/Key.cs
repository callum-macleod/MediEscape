using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "Key", menuName = "Item/Key")]
public class Key : ItemInfo
{
    [SerializeField] Thief player;
}
