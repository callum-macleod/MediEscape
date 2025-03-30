using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Character/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    [Header("Enemy Info")]
    public new string name;
    public GameObject prefab;
    
    [Header("Attributes")]
    public int health;
    public int damage;
    public float FOVRange;
    public float poximityRadius;

    [Header("Items")]
    public ItemInfo itemHeld;
}
