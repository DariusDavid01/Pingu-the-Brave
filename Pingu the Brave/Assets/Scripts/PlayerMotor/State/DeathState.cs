using System.Collections;
using UnityEngine;
public class DeathState : BaseState
{

    [SerializeField] private Vector3 knockbackForce = new Vector3(0, 4, -3);
    private Vector3 currentKnockback;
    public override void Construct()
    {
        motor.anim?.SetTrigger("Death");
        colliders = GameObject.Find("WorldGeneration").GetComponentsInChildren<Collider>();
        foreach (Collider childCollider in colliders)
        {
            if (childCollider.tag == "ramp")
                childCollider.enabled= false;
        }
        //motor.isPaused = false;
        //motor.anim.enabled = false;
        if (motor.died == 0)
            currentKnockback = knockbackForce;
        else
        {
            currentKnockback = Vector3.zero;
        }
    }
    public override void Destruct()
    {
        /*colliders = GameObject.Find("WorldGeneration").GetComponentsInChildren<Collider>();
        foreach (Collider childCollider in colliders)
        {
            childCollider.enabled = true;
        }*/
        motor.anim?.ResetTrigger("Fall");
        motor.anim?.ResetTrigger("Jump");
    }
    public override Vector3 ProcessMotion()
    {
        //Debug.Log(motor.died);
        Vector3 m = currentKnockback;
        currentKnockback = new Vector3(0, currentKnockback.y -= motor.gravity * Time.deltaTime, currentKnockback.z += 2.0f * Time.deltaTime);
            if (currentKnockback.z > 0)
            {
                currentKnockback.z = 0;
                GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
            }
            return currentKnockback;
        }
    
}
