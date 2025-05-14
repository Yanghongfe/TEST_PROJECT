using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_wall_Junmp : Playerstate
{
    public Player_wall_Junmp(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        player.Setvelocity(5*-player.facingDir,player.Jumpforce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.airstate);

        }

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idelestate);

    }

}
