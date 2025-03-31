using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "StealthPotion", menuName = "Consumable/StealthPotion")]

public class StealthPotion : ItemInfo
{
    
    public float timerLimit;

    public void Use(GameObject player)
    {
        
    }

    private IEnumerator EnableStealth(GameObject player)
    {
        player.layer = LayerMask.NameToLayer("Default");

        yield return new WaitForSeconds(timerLimit);

        player.layer = LayerMask.NameToLayer("Player");
    }

}