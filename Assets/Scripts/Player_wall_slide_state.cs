using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_wall_slide_state : Playerstate
{
    public Player_wall_slide_state(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.walljump);
            return;
        }


        if (xinput != 0 && player.facingDir != xinput)
            stateMachine.ChangeState(player.idelestate);
        
        if(yinput <0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idelestate);



    }


}
