using System.Collections;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor Instance { get { return instance; } }
    private static PlayerMotor instance;
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded=true;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseSidewaySpeed = 10.0f;
    public float terminalVelocity = 20.0f;
    public float gravity = 14.0f;


    //speed modifier
    public float baseRunSpeed = 5.0f;
    public float originalSpeed = 5.0f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    public CharacterController controller;
    public CharacterController ctrl;
    private BaseState state;
    public Animator anim;
    public AudioClip hitSound;

    public int died = 0;

    public bool isPaused;
    private void Start()
    {
        originalSpeed = 5.0f;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        state = GetComponent<RunningState>();
        state.Construct();
        isPaused = true;
        isGrounded = controller.isGrounded;
        died = 0;

        foreach (MeshCollider go in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
            go.enabled = true;
    }
    private void Update()
    {
        if (!isPaused)
        {
            UpdateMotor();
            //isGrounded = true;
            if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
            {
                speedIncreaseLastTick = Time.time;
                originalSpeed += speedIncreaseAmount;

            }
        }
        //c2 = controller;
        //c2.height = 42.2f;
        //if (c2.isGrounded)
        //isGrounded = true;
        //waitbit();

    }
    public Vector3 ProcessMotionTest()
    {
        //apply gravity
        ApplyGravity();

        //craete our return vector
        Vector3 m = Vector3.zero;

        m.x = SnapToLane();
        m.y = verticalVelocity-6;
        m.z = originalSpeed;

        return m;
    }

    public void SetOriginalSpeed()
    {
        baseRunSpeed = 5.0f;
    }

    private void UpdateMotor()
    {
        //check if we're grounded
        isGrounded = controller.isGrounded;
        if (ctrl.isGrounded)
        {
            //Debug.Log("it is grounded");
            isGrounded = true;
        }
        //how should we be moving right now? based on state
        moveVector = state.ProcessMotion();

        //are we trying to change state?
        state.Transition();

        //feed our animator some values
        //isGrounded = true;
        anim?.SetBool("IsGrounded", isGrounded);
        //anim.SetBool("IsGrounded", true);
        anim?.SetFloat("Speed", Mathf.Abs(moveVector.z));
        //move tehe player
        controller.Move(moveVector * Time.deltaTime);
        ctrl.Move(ProcessMotionTest() * Time.deltaTime);
        //if (isPaused) Debug.Log("total deaths = " + totalDeaths);
    }
    public float SnapToLane()
    {
        float r = 0.0f;
        if (transform.position.x != (currentLane*distanceInBetweenLanes))
        {
            float deltaToDesiredPosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
        }
        else
        {
            r = 0;
        }

        return r;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

    public void PausePlayer()
    {
        isPaused = true;
    }
    public void ResumePlayer()
    {
        isPaused = false;
        died = 0;

        foreach (MeshCollider go in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
            go.enabled = true;
    }
    public void RespawnPlayer()
    {
        foreach (MeshCollider go in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
            go.enabled = true;
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameCamera.respawn);
    }
    public void ResetPlayer()
    {
        foreach (MeshCollider go in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
            go.enabled = true;
        originalSpeed = 5.0f;
        currentLane = 0;
        transform.position = Vector3.zero;
        anim?.SetTrigger("Idle");
        PausePlayer();
        ChangeState(GetComponent<RunningState>());
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);
        if (hitLayerName == "Death" && died==0)
        {
            AudioManager.Instance.PlaySFX(hitSound);
            ChangeState(GetComponent<DeathState>());
            died = 1; 
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if (go.name == "Coin Detector")
                    go.SetActive(false);
            }
        }
        if ((hit.gameObject.tag == "death" || hit.gameObject.name == "Death") && died==0)
        {
            AudioManager.Instance.PlaySFX(hitSound);
            Debug.Log("death");
            ChangeState(GetComponent<DeathState>());
            died = 1;
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if (go.name == "Coin Detector")
                    go.SetActive(false);
            }

            foreach (MeshCollider go in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
                go.enabled = false;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ramp" || other.gameObject.name == "ramp")
        {
            Debug.Log("Ramp");
            ChangeState(GetComponent<BouncingState>());
        }
        if ((other.gameObject.tag == "death" || other.gameObject.name == "Death") && died==0)
        {
            AudioManager.Instance.PlaySFX(hitSound);
            Debug.Log("death");
            ChangeState(GetComponent<DeathState>());
            died = 1;
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if (go.name == "Coin Detector")
                    go.SetActive(false);
            }
            foreach (MeshCollider go in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
                go.enabled = false;
        }
    }



}
