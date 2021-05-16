using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingState : BaseState
{
    public float jumpForce = 20.0f;

    public override void Construct()
    {
        motor.verticalVelocity = jumpForce;
        motor.anim?.SetTrigger("Jump");
    }

    public override Vector3 ProcessMotion()
    {
        //apply gravity
        motor.ApplyGravity();

        //craete our return vector
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.originalSpeed;

        return m;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }
        if (InputManager.Instance.SwipeDown)
        {
            motor.verticalVelocity = -jumpForce;
        }
        if (motor.verticalVelocity < 0)
            motor.ChangeState(GetComponent<FallingState>());

    }
}
