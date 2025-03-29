using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Thief : HealthyEntity
{
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

    Rigidbody2D _rigidBody;
    Rigidbody2D rigidBody
    {
        get
        {
            if (_rigidBody == null)
                _rigidBody = GetComponent<Rigidbody2D>();
            return _rigidBody;
        }
    }

    // hitbox of child object
    EdgeCollider2D _attackHitbox;
    EdgeCollider2D attackHitbox
    {
        get
        {
            if (_attackHitbox == null)
                _attackHitbox = GetComponentInChildren<EdgeCollider2D>();
            return _attackHitbox;
        }
    }

    [SerializeField] List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    // GENERAL PROPERTIES AND FIELDS
    Vector2 move;
    float moveSpeed = 2.5f;

    int facingDirection = 1;

    // determines how early can you buffer an attack/ability
    float inputBufferMaxPeriod = 0.2f;

    float attackCooldown = 0.65f;
    float currentAttackCooldown = 0f;
    bool attackBuffered = false;
    bool attacking { get { return currentAttackCooldown > 0; } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("TrHurt");
        if (Input.GetKey(KeyCode.Mouse0) || attackBuffered)
            Attack();

        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");
        move = new Vector2(xMov, yMov).normalized;

        if (currentAttackCooldown > 0)
            currentAttackCooldown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = move * moveSpeed;
        SetFacingDirection();

        if (move.magnitude > 0 && currentAttackCooldown <= 0) animator.SetTrigger("TrWalk");
        if (move.magnitude == 0) animator.SetTrigger("TrIdle");
    }

    // determines which direction the player is facing
    int SetFacingDirection()
    {
        // if not moving horiztonally, change nothing.
        if (move.x == 0)
            return facingDirection;

        // return 1 if moving left, return -1 if moving right;
        int newDirection = -1 * (int)(move.x / Mathf.Abs(move.x));

        if (newDirection == facingDirection)
            return facingDirection;

        // flip character if necessary and return (and set) new direction value
        transform.localScale = new Vector3(newDirection, 1, 1);
        return facingDirection = newDirection;
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        // some walls (front and back) require us to dynamically update the draw order of sprites
        // while in contact with such walls:
        if (collision.tag == "RequiresDrawOrder")
        {
            int tilemapOrder = collision.GetComponent<TilemapRenderer>().sortingOrder;  // get draw order

            // is wall above or below player?
            if (collision.bounds.center.y > transform.position.y)
                tilemapOrder++;
            else
                tilemapOrder--;

            // update player sprites draw order
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.sortingOrder = tilemapOrder;
            }
        }
    }

    void Attack()
    {
        if (currentAttackCooldown > 0)
        {
            // if attack is about to come off cooldown: buffer attack
            if (!attackBuffered && currentAttackCooldown < inputBufferMaxPeriod)
                attackBuffered = true;

            return;
        }

        attackBuffered = false;
        currentAttackCooldown = attackCooldown;
        currentAttackCooldown += Time.deltaTime;  // ensures that `attacking` returns true immediately
        animator.SetTrigger("TrAttack");
        attackHitbox.enabled = true;
        Invoke(nameof(DisableAttackHitbox), 1);
    }

    void DisableAttackHitbox()
    {
        attackHitbox.enabled = false;
    }
}
