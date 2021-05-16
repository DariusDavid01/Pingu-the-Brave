using UnityEngine;

public class RunningState : BaseState
{

    public AudioClip jumpSound;
    public override void Construct()
    {
        motor.verticalVelocity = 0;
    }
    public override Vector3 ProcessMotion()
    {
        motor.anim?.ResetTrigger("Running");
        Vector3 m = Vector3.zero;
        m.x = motor.SnapToLane();
        m.y = -1.0f;
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
        if (InputManager.Instance.SwipeUp && motor.isGrounded)
        {
            AudioManager.Instance.PlaySFX(jumpSound);
            motor.ChangeState(GetComponent<JumpingState>());
        }
        if (!motor.isGrounded)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
        if (InputManager.Instance.SwipeDown)
        {
            motor.ChangeState(GetComponent<SlidingState>());
        }
    }
}
