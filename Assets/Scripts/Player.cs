using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [Header("Attack Detail")]
    public Vector2[] attackMovement;



    public bool isbusy { get; private set; }


    [Header("moveinfo")]
    public float movespeed = 12;
    public float Jumpforce;




    [Header("Dashinfo")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashspeed;
    public float dashDuration;
    public float dashDir { get; private set; }






    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatisGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }



    #endregion


    #region States
    public Playerstatemachine statemachine {  get; private set; }
    public Player_idle_state idelestate { get; private set; }
    public Player_move_state movestate { get; private set; }
    public Player_jumpstate jumpstate { get; private set; }
    public Player_airstate airstate { get; private set; }
    public Player_dash_state dashstate { get; private set; }
    public Player_wall_slide_state wallslide { get; private set; }
    public Player_wall_Junmp walljump { get; private set; }
    public Player_primary_attack_state primaryattck { get; private set; }


    #endregion

    private void Awake()
    {
        statemachine = new Playerstatemachine();
        idelestate = new Player_idle_state(this,statemachine,"Idle");
        movestate = new Player_move_state(this, statemachine, "Move");
        jumpstate = new Player_jumpstate(this, statemachine, "Jump");
        airstate = new Player_airstate(this, statemachine, "Jump");
        dashstate = new Player_dash_state(this, statemachine, "Dash");
        wallslide = new Player_wall_slide_state(this, statemachine, "Wallslide");
        walljump = new Player_wall_Junmp(this, statemachine, "Jump");

        primaryattck = new Player_primary_attack_state(this, statemachine, "Attack");

    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        statemachine.initialize(idelestate);

    }
    private void Update()
    {
        statemachine.currentstate.Update();

        checkForDashInput();

    }


    public IEnumerator BusyFor(float _seconds)
    {
        isbusy = true;

        Debug.Log("Is Busy");

        yield return new WaitForSeconds(_seconds);
        Debug.Log("Not busy");

        isbusy = false;

    }

    public void AnimationTriiger()=> statemachine.currentstate.AnimationFinishTrigger();

    private void checkForDashInput()
    {
        if(IsWallDetected())
        {

            return;
        }
        dashUsageTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.LeftShift)&& dashUsageTimer<0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;


            statemachine.ChangeState(dashstate);

        }
    }


    #region Velocity

    public void Zero_velcocity() => rb.velocity = new Vector2 (0, 0);

    public void Setvelocity(float _xVelocity, float _yVelocity)
    {

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    #endregion


    #region Collision

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatisGround);
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));


    }
    #endregion

    #region  Flip

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();

    }
    #endregion
}
