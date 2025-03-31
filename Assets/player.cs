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

    [SerializeField] private bool ismove;
    private float xinput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        xinput = UnityEngine.Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xinput*movespeed, rb.velocity.y);


        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

        ismove = rb.velocity.x != 0;

        anim.SetBool("Ismoveing", ismove);
    }
}
