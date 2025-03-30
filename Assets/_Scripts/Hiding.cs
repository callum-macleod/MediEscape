using UnityEngine;

public class Hiding : MonoBehaviour
{
    private bool playerNearby = false;
    private bool playerHidden = false;
    private GameObject player;
    private Rigidbody2D playerRB;

    private int originalLayer;
    private RigidbodyConstraints2D originalConstraints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            playerNearby = true;
            player = collision.gameObject;
            playerRB = player.GetComponent<Rigidbody2D>();
            originalLayer = player.layer;
            originalConstraints = playerRB.constraints;
            Debug.Log($"{player.name} is near {gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !playerHidden){
            playerNearby = false;
            player = null;
            Debug.Log($"Player no longer near {gameObject.name}");
        }
    }

    void Update()
    {
        if(playerNearby && Input.GetKeyDown(KeyCode.H))
            ToggleHiding();
        else if(playerHidden && Input.GetKeyDown(KeyCode.H))
            ToggleHiding();
    }

    private void ToggleHiding()
    {
        if(player == null) return;

        playerHidden = !playerHidden;

        if(playerHidden){
            SetChildrenSpritesVisibility(player, false);
            player.GetComponent<Collider2D>().enabled = false;
            DisableMovement(true);
            player.layer = LayerMask.NameToLayer("Default");
            Debug.Log($"{player.name} is hiding in {gameObject.name}");
        }else{
            SetChildrenSpritesVisibility(player, true);
            player.GetComponent<Collider2D>().enabled = true;
            DisableMovement(false);
            player.layer = originalLayer;
            Debug.Log($"{player.name} left {gameObject.name}");
        }
    }

    private void SetChildrenSpritesVisibility(GameObject parent, bool _switch)
    {
        SpriteRenderer[] sprites = parent.GetComponentsInChildren<SpriteRenderer>();

        foreach(var sprite in sprites)
            sprite.enabled = _switch;
    }

    private void DisableMovement(bool _switch)
    {
        if(_switch)
            playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
        else
            playerRB.constraints = originalConstraints;
    }
}
