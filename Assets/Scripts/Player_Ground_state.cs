using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ground_state : Playerstate
{
    public Player_Ground_state(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryattck);

        }

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airstate);



        if (Input.GetKeyDown(KeyCode.Space)&&player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpstate);
 
    }
}
