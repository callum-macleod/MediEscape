using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Inventory/HealthPotion")]

public class HealthPotion : Item
{
    public int healAmount = 2;
    public void Use(PlayerHealth playerHealth)
    {
        if (playerHealth != null)
        {
            Debug.Log($"Healing for {healAmount} HP");
            playerHealth.Heal(healAmount);
        }
    }

    public override void Use()
    {
        Debug.LogWarning("Don't use this method. Use(PlayerHealth).");
    }

}
