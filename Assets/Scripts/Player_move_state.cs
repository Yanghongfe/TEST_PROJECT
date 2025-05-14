using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move_state : Player_Ground_state
{
    public Player_move_state(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
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

        player.Setvelocity(xinput * player.movespeed, rb.velocity.y);



        if (xinput==0|| player.IsWallDetected())
        {
            player.statemachine.ChangeState(player.idelestate);
        }
    }
}
