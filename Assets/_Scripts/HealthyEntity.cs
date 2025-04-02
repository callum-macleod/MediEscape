using System.ComponentModel;
using UnityEngine;

public class HealthyEntity : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 1;
    public int MaxHealth { get { return maxHealth; } }

    protected int currentHealth { get; set; } = int.MaxValue;
    public int CurrentHealth {  get { return currentHealth; } }
    protected bool dead = false;

    [Category("Audio")]
    [SerializeField] protected AudioClip takeDmg;
    [SerializeField] protected AudioClip attack1;

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
        if (currentHealth == int.MaxValue)
            currentHealth = maxHealth;
    }

    public void RecieveDamage(int damage)
    {
        AudioMgr.Instance.PlayHitSound(transform);
        print($"Recieved {damage}");
        currentHealth -= damage;
        AudioSource.PlayClipAtPoint(takeDmg, transform.position);

        if (!CheckIfReadyToDie())
            animator.SetTrigger("TrHurt");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Clamp to max
        Debug.Log("Healed! Current health: " + currentHealth);
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
    protected virtual void Die()
    {
        print($"{name}: Dying");
        animator.SetTrigger("TrDie");
        dead = true;
        Destroy(gameObject, 0.8f);
    }
}
