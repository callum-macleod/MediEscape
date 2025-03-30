using UnityEditor.Rendering;
using UnityEngine;

public class ThiefKnives : MonoBehaviour
{
    public float damage = 40;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("knifed");
        if (collision.gameObject.layer != (int)Layers.Enemies)
            return;

        collision.gameObject.GetComponent<HealthyEntity>().RecieveDamage(damage);
    }
}
