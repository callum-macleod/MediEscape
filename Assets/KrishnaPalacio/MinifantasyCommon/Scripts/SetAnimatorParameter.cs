using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minifantasy
{
    public class SetAnimatorParameter : MonoBehaviour
    {
        private Animator animator;

        public string currentParameterName = "Idle";
        public float x = 1;
        public float y = 1;
        public float waitTime = 0f;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            Invoke("ToggleAnimatorParameter", waitTime);
            UpdateXY();
        }

        public void ToggleAnimatorParameter()
        {
            animator.SetBool(currentParameterName, true);
        }
        public void ToggleAnimatorParameter(string newParam,  bool newBool)
        {
            animator.SetBool(newParam, newBool);
        }

        public void UpdateAnimatorParameter(string newParam)
        {
            animator.SetBool(currentParameterName, false);
            animator.SetBool(newParam, true);
        }

        public void UpdateXY()
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }

        public void SetSpeed(Vector2 movement)
        {
            float speed = (movement.magnitude > 0) ? 1 : 0;
            animator.SetFloat("Speed", speed);
        }

        public void SetAttackDirection(Vector2 movement)
        {
            int direction = 1;
            if (movement == (Vector2.down + Vector2.right).normalized)
                direction = 1;
            if (movement == (Vector2.down + Vector2.left).normalized)
                direction = 2;
            if (movement == (Vector2.up + Vector2.left).normalized)
                direction = 3;
            if (movement == (Vector2.up + Vector2.right).normalized)
                direction = 4;

            animator.SetFloat("Direction", direction);
        }
    }
}