using UnityEngine;

public class HealthyEntity : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 1;

    protected float currentHealth;
    public float CurrentHealth {  get { return currentHealth; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void RecieveDamage(float damage)
    {
        currentHealth -= damage;
        CheckIfReadyToDie();
    }

    protected void CheckIfReadyToDie()
    {
        if (currentHealth < 0)
        {
            Die();
        }
    }
    protected void Die()
    {

    }
}
