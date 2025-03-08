using Unity.VisualScripting;
using UnityEngine;

public class Thief : MonoBehaviour
{
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

    Vector2 move;
    float moveSpeed = 2.5f;

    int facingDirection = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Hurt");

        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");
        move = new Vector2(xMov, yMov).normalized;

    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = move * moveSpeed;
        transform.localScale = new Vector3(SetFacingDirection(), 1, 1);

        if (move.magnitude > 0) animator.SetTrigger("Walk");
        if (move.magnitude == 0) animator.SetTrigger("Idle");
    }

    // returns 1 if facing move
    int SetFacingDirection()
    {
        // if not moving horiztonally, change nothing.
        if (move.x == 0) return facingDirection;

        // return 1 if moving left, return -1 if moving right;
        return facingDirection = -1 * (int)(move.x / Mathf.Abs(move.x));
    }
}
