using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected Animator anim;

    protected int FaceDir = 1;
    protected bool FaceRight = true;


    [Header("collision info")]
    [SerializeField] protected Transform Groudcheck;
    [SerializeField] protected float Groudcheckdistance;
    [Space]
    [SerializeField] protected Transform wallcheck;
    [SerializeField] protected float wallcheckdistance;
    [SerializeField] protected LayerMask whatisground;

    protected bool IsGrounded;
    protected bool iswalldetected;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        if (wallcheck == null)
            wallcheck = transform;
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }


    protected virtual void CollisionChecks()
    {
        IsGrounded = Physics2D.Raycast(Groudcheck.position, Vector2.down, Groudcheckdistance, whatisground);
        iswalldetected = Physics2D.Raycast(wallcheck.position, Vector2.right, wallcheckdistance, whatisground);
        Debug.Log(IsGrounded);
    }

    protected virtual void Flip()
    {
        FaceDir = FaceDir * -1;
        FaceRight = !FaceRight;
        transform.Rotate(0, 180, 0);
    }



    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(Groudcheck.position, new Vector3(Groudcheck.position.x, Groudcheck.position.y - Groudcheckdistance));
        Gizmos.DrawLine(wallcheck.position, new Vector3(wallcheck.position.x +  wallcheckdistance * FaceDir, wallcheck.position.y));

    }


}
