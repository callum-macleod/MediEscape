using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "StealthPotion", menuName = "Consumable/StealthPotion")]

public class StealthPotion : ItemInfo
{
    
    public float timerLimit;

    public void Use(GameObject player)
    {
        AudioMgr.Instance.PlayStealthSound(player.transform);
    }

}