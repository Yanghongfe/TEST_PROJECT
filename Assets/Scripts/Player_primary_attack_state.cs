using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_primary_attack_state : Playerstate
{

    private int comboCounter;

    private float LastTimeAttacked;

    private float combowindow = 2;


    public Player_primary_attack_state(Player player, Playerstatemachine _stateMachine, string _animBoolName) : base(player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        if(comboCounter>2|| Time.time >= LastTimeAttacked+combowindow) {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);

        #region Choose attack direction
        float attackDir = player.facingDir;
        if (xinput != 0)
        {
            attackDir = xinput;
        }
        #endregion

        player.Setvelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = .1f;


    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);

        comboCounter++;
        LastTimeAttacked = Time.time;

    }

    public override void Update()
    {
        base.Update();


        if (stateTimer < 0)
        {
            player.Zero_velcocity();
        }

        if(trriger_called)
            stateMachine.ChangeState(player.idelestate);


    }


}
