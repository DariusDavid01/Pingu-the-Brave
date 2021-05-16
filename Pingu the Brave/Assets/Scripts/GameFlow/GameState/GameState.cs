using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager brain;
    protected virtual void Awake()
    {
        brain = GetComponent<GameManager>();
        //DontDestroyOnLoad(this);
    }
    public virtual void Construct()
    {
        
    }

    public virtual void Destruct()
    {

    }

    public virtual void UpdateState()
    {

    }
}
