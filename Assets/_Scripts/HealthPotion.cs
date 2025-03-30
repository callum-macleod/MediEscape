using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Inventory/HealthPotion")]

public class HealthPotion : Item
{
    public int healAmount = 2;
    //public PlayerHealth playerHealth;

    public void Use(PlayerHealth playerHealth)
    {
        if (playerHealth != null)
        {
            Debug.Log($"Healing for {healAmount} HP");
            playerHealth.Heal(healAmount);
        }
    }

    //public override void Use()
    //{
    //    base.Use(); //keeps debug from base code
    //    Debug.Log($"Healing for {healAmount} HP"); //debug to make sure new stuff works on use

    //    GameObject player = GameObject.FindWithTag("Player");
    //    if (player != null)
    //    {
    //        PlayerHealth health = player.GetComponent<PlayerHealth>();
    //        if (health != null)
    //        {
    //            health.Heal(healAmount);
    //        }
    //    }
    //}

    public override void Use()
    {
        Debug.LogWarning("Don't use this method. Use(PlayerHealth).");
    }

}
