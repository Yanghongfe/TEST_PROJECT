using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_dash_state : Playerstate
{
    public Player_dash_state(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.Setvelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallslide);


        player.Setvelocity(player.dashspeed * player.dashDir, 0);


        if (stateTimer < 0)
            stateMachine.ChangeState(player.idelestate);
    }
}
