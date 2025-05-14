using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player_jumpstate : Playerstate
{
    public Player_jumpstate(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.Jumpforce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airstate);
    }
}
