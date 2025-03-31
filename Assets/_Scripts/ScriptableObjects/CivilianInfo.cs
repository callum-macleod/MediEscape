using UnityEngine;

[CreateAssetMenu(fileName = "Civilian", menuName = "NPC/Civilian")]
public class CivilianInfo : ScriptableObject
{
    public string civilName;
    public ItemInfo tradable;
}
