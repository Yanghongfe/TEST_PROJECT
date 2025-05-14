using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstatemachine 
{

    public Playerstate currentstate { get; private set; }

    public void initialize(Playerstate _startstate)
    {
        currentstate = _startstate;
        currentstate.Enter();

    }
    public void ChangeState(Playerstate _newstate)
    {
        currentstate.Exit();
        currentstate = _newstate;
        currentstate.Enter();
    }

}
