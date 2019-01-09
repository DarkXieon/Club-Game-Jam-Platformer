using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationVelocityTracker : MonoBehaviour
{
    public float MinVelocityThreshold;

    private Animator animator;
    private Rigidbody2D body;
    private bool isForcingIdle;

    private const string upwardForce = "Upward Force";
    private const string downwardForce = "Downward Force";
    private const string horizontalForce = "Horizontal Force";
    private const string dead = "Dead";
    private const string horizontalVelocity = "Horizontal Speed";

    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (!isForcingIdle)
        {
            if (body.velocity.y > MinVelocityThreshold)
            {
                animator.SetBool(upwardForce, true);
                animator.SetBool(downwardForce, false);
            }
            else if (body.velocity.y < -MinVelocityThreshold)
            {
                animator.SetBool(upwardForce, false);
                animator.SetBool(downwardForce, true);
            }
            else
            {
                animator.SetBool(upwardForce, false);
                animator.SetBool(downwardForce, false);
            }
        }
    }

    public void StartRunning(float speed)
    {
        if(!isForcingIdle)
        {
            animator.SetBool(horizontalForce, true);
            animator.SetFloat(horizontalVelocity, speed);
        }
    }

    public void StopRunning()
    {
        animator.SetBool(horizontalForce, false);
        animator.SetFloat(horizontalVelocity, 0f);
    }

    public void Die()
    {
        animator.SetBool(dead, true);
        //ForceIdle(true);
    }

    public void ForceIdle(bool forceIdle)
    {
        isForcingIdle = forceIdle;

        if(forceIdle)
        {
            animator.SetBool(upwardForce, false);
            animator.SetBool(downwardForce, false);
            StopRunning();
        }
    }
}
