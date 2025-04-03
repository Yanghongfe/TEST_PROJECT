using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;


    [SerializeField]private float movespeed;
    [SerializeField] private float jumpforce;
    private float xinput;

    private int FaceDir = 1;
    private bool FaceRight = true;


    [Header("collision info")]
    [SerializeField] private float Groudcheck;
    [SerializeField] private LayerMask whatisground;
    private bool IsGrounded;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();
        FlipControler();
        AnimatorControllers();

    }

    private void CollisionChecks()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, Groudcheck, whatisground);
        Debug.Log(IsGrounded);
    }

    private void CheckInput()
    {

        xinput = UnityEngine.Input.GetAxisRaw("Horizontal");

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xinput * movespeed, rb.velocity.y);
    }

    private void Jump()
    {
        if ((IsGrounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
    }

    private void AnimatorControllers()
    {

        bool ismove = rb.velocity.x != 0;
        anim.SetBool("Ismoveing", ismove);
        anim.SetBool("isGrounded", IsGrounded);

        anim.SetFloat("Y_velocity",rb.velocity.y);




    }

    private void Flip()
    {
        FaceDir = FaceDir * -1;
        FaceRight = !FaceRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipControler()
    {
        if(rb.velocity.x > 0 && !FaceRight)
        {
            Flip();

        }

        else if (rb.velocity.x < 0 && FaceRight)
        {
            Flip();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x, transform.position.y- Groudcheck));


    }




}
