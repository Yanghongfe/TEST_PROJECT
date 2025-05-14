using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_airstate : Playerstate
{
    // Start is called before the first frame update
    public Player_airstate(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
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

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallslide);



        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idelestate);
        if (xinput != 0)
            player.Setvelocity(player.movespeed * .8F * xinput, rb.velocity.y);


    }
}
