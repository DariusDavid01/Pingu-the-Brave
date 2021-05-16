using UnityEngine;

public class FallingState : BaseState
{
    public override void Construct()
    {
        motor.anim?.SetTrigger("Fall");
        //motor.isGrounded = false;
        //motor.anim.SetBool("IsGrounded", false);
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
        if (motor.isGrounded)
            motor.ChangeState(GetComponent<RunningState>());
        if (InputManager.Instance.SwipeUp && motor.isGrounded == true)
        {
            //AudioManager.Instance.PlaySFX(jumpSound);
            motor.ChangeState(GetComponent<JumpingState>());
        }
    }
}
