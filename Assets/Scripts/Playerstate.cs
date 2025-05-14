using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstate 
{

    protected Playerstatemachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;



    protected float xinput;
    protected float yinput;

    private string animBoolName;

    protected float stateTimer;
    protected bool trriger_called;


    public Playerstate(Player player ,Playerstatemachine _stateMachine, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb= player.rb;
        trriger_called = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("y_velocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }

    public virtual void AnimationFinishTrigger()
    {
        trriger_called= true;

    }

}
