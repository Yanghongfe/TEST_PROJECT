using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class player : Entity
{


    [Header("Move info")]
    [SerializeField]private float movespeed;
    [SerializeField] private float jumpforce;


    [Header("Dash info")]
    [SerializeField] private float dashspeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashtime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashCooldownTimer;


    [Header("Attack info")]
    [SerializeField] private float comboTime = .3f;
    private bool isAttacking;
    private int comboCounter;
    private float comboTimeWindow;




    private float xinput;



    protected override void Start()
    {
        base.Start();
    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();
        dashtime = dashtime - Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;

        FlipControler();
        AnimatorControllers();

    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }




    }

    public void Dashability()
    {
        if (dashCooldownTimer < 0&& !isAttacking )
        {
            dashCooldownTimer = dashCooldown;
            dashtime = dashDuration;
        }
    }







    private void CheckInput()
    {

        xinput = UnityEngine.Input.GetAxisRaw("Horizontal");

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


        if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
        {
            startattack();

        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) )
        {
            Dashability();
        }

    }

    private void startattack()
    {
        if(!IsGrounded)
        {
            return;
        }

        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity =new Vector2(0,0);
        }
        else if(dashtime>0) {
            rb.velocity = new Vector2(FaceDir * dashspeed,0);
        }
        else
        {
            rb.velocity = new Vector2(xinput * movespeed, rb.velocity.y);
        }

    }

    private void Jump()
    {
        if ((IsGrounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
    }

    private void AnimatorControllers()
    {

        bool ismove = rb.velocity.x != 0;
        anim.SetBool("Ismoveing", ismove);
        anim.SetBool("isGrounded", IsGrounded);
        anim.SetBool("If_dash", dashtime>0);
        anim.SetBool("is_attack", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
        anim.SetFloat("Y_velocity",rb.velocity.y);

    }


    private void FlipControler()
    {
        if(rb.velocity.x > 0 && !FaceRight)
        {
            Flip();

        }

        else if (rb.velocity.x < 0 && FaceRight)
        {
            Flip();

        }
    }

 

}
