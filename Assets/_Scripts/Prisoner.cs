using Minifantasy;
using UnityEngine;

public class Prisoner : MonoBehaviour
{
    SetAnimatorParameter _animParam;
    SetAnimatorParameter animParam
    {
        get
        {
            if (_animParam == null)
                _animParam = GetComponent<SetAnimatorParameter>();
            return _animParam;
        }
    }

    Vector2 move = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animParam.UpdateAnimatorParameter("Attack");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animParam.UpdateAnimatorParameter("Idle");
        }

        int xMov = 0;
        int yMov = 0;
        //float xMov = Input.GetAxisRaw("Horizontal");
        //float yMov = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.W)) yMov += 1; 
        if (Input.GetKey(KeyCode.S)) yMov -= 1; 
        if (Input.GetKey(KeyCode.D)) xMov += 1; 
        if (Input.GetKey(KeyCode.A)) xMov -= 1;

        Vector2 newMove = new Vector2(xMov, yMov).normalized;
        if (move != newMove && newMove != Vector2.zero)
        {
            move = newMove;
            animParam.x = move.x;
            animParam.y = move.y;
            animParam.UpdateXY();
            animParam.SetAttackDirection(move);
        }
        animParam.SetSpeed(newMove);
    }

    private void FixedUpdate()
    {
    }
}
