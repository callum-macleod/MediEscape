using UnityEngine;

public class HealthyEntity : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 1;

    protected float currentHealth = float.MaxValue;
    public float CurrentHealth {  get { return currentHealth; } }
    protected bool dead = false;



    // COMPONENTS
    Animator _animator;
    Animator animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentHealth == float.MaxValue)
            currentHealth = maxHealth;
    }

    public void RecieveDamage(float damage)
    {
        print($"Recieved {damage}");
        currentHealth -= damage;

        if (!CheckIfReadyToDie())
            animator.SetTrigger("TrHurt");
    }

    protected bool CheckIfReadyToDie()
    {
        if (currentHealth < 0)
        {
            Die();
            return true;
        }

        return false;
    }
    protected void Die()
    {
        print($"{name}: Dying");
        animator.SetTrigger("TrDie");
        dead = true;
        Destroy(gameObject, 0.8f);
    }
}
