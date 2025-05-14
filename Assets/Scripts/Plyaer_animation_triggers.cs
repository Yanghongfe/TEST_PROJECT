using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyaer_animation_triggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();    

    private void AnimationTrigger()
    {
        player.AnimationTriiger();
    }
}
