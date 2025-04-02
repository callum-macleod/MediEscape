using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    [SerializeField] public Transform teleportPoint;
    private GameObject player;
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
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            if (player != null && teleportPoint != null)
            {
                player.transform.position = teleportPoint.position;
            }
        }
    }
}
