using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Consumable/HealthPotion")]

public class HealthPotion : ItemInfo
{
    public int healAmount = 2;
    public void Use(HealthyEntity playerHealth)
    {
        if (playerHealth != null)
        {
            Debug.Log($"Healing for {healAmount} HP");
            playerHealth.Heal(healAmount);
            AudioMgr.Instance.PlayHealthSound(playerHealth.transform);
        }
    }

}
