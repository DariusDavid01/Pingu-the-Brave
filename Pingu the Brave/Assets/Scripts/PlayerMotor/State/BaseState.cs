using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected Collider[] colliders;
    protected PlayerMotor motor;
    public virtual void Construct() {}
    public virtual void Destruct() {}
    public virtual void Transition() {}

    private void Awake()
    {
        
        motor = GetComponent<PlayerMotor>();
    }
    public virtual Vector3 ProcessMotion()
    {
        return Vector3.zero;
    }
}
