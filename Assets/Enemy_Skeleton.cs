using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{

    [Header("Move info")]
    [SerializeField] private float movespeed;


    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (!IsGrounded || iswalldetected)
            Flip();

        rb.velocity = new Vector2(movespeed * FaceDir, rb.velocity.y);
    }


}
