using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Inventory/HealthPotion")]

public class HealthPotion : ItemInfo
{
    public int healAmount = 2;
    public void Use(HealthyEntity playerHealth)
    {
        if (playerHealth != null)
        {
            Debug.Log($"Healing for {healAmount} HP");
            playerHealth.Heal(healAmount);
        }
    }

}
