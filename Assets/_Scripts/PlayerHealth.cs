using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int playerHealth = 10;
    private void Start()
    {
        playerHealth = maxHealth;
    }
}
