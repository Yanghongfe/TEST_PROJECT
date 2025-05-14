using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_idle_state : Player_Ground_state
{
    public Player_idle_state(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Zero_velcocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (xinput == player.facingDir && player.IsWallDetected())
            return;

        if (xinput !=0 &&!player.isbusy)
        {
            player.statemachine.ChangeState(player. movestate);
        }
    }
}
