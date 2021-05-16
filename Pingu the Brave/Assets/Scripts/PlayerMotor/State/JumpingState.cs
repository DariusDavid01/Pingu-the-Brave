using UnityEditor;
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce =7.0f;
    public override void Construct()
    {
        motor.verticalVelocity = jumpForce;
        motor.anim?.SetTrigger("Jump");
        //ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        //EditorUtility.CopySerialized(ctrl, GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>());

        //ctrl.height = 2.6f;

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
        if (InputManager.Instance.SwipeUp && motor.isGrounded == true)
        {
            //AudioManager.Instance.PlaySFX(jumpSound);
            motor.ChangeState(GetComponent<JumpingState>());
        }
        if (InputManager.Instance.SwipeUp && motor.ctrl.isGrounded == true)
        {
            motor.anim?.SetBool("IsGrounded", true);
            motor.ChangeState(GetComponent<JumpingState>());
        }
        if (motor.verticalVelocity < 0)
            motor.ChangeState(GetComponent<FallingState>());
    }
}
