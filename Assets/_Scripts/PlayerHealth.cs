using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int playerHealth = 10;
    private void Start()
    {
        playerHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Min(playerHealth, maxHealth); // Clamp to max
        Debug.Log("Healed! Current health: " + playerHealth);
    }
}
