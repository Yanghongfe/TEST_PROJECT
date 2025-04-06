using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{

    private player player;






    void Start()
    {
        player = GetComponentInParent<player>();
    }

    private void AnimationTrigger()
    {
        player.AttackOver();
    }


}
