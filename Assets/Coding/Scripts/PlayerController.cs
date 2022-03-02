//------------------------------------------------------------------------------
//
// File Name:	PlayerController.cs
// Author(s):	Ryan Schepplar
//              Gavin Cooper (gavin.cooper)
// Project:	    Raccoon
// Course:	    WANIC VGP2
//
// Copyright ©️ 2022 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //speed and movement variables
    public float speed;
    public float airSpeed;
    private int direction;
    //grab this to adjust physics
    private Rigidbody2D myRb;

    //things for ground checking
    private bool isGrounded = false;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //jump things
    public float jumpForce;
    private bool jumpPressed = true;

    private float jumpTimer = 0;
    public float jumpTime = 0.2f;

    public float groundDrag = 5;
    public float airDrag = 1;

    private AudioSource myAud;
    public AudioClip jumpNoise;

    //animation
    private Animator myAnim;

    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode jumpKey = KeyCode.Space;


    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAud = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }

    //Update is called once per frame
    private void Update()
    {
        //check if jump can be triggered
        if (Input.GetKeyDown(jumpKey) && jumpPressed == false && isGrounded == true)
        {
            myAud.PlayOneShot(jumpNoise);
            myRb.drag = airDrag;
            myRb.velocity = (Vector2.up * jumpForce) + new Vector2(myRb.velocity.x, 0);
            jumpPressed = true;
        }
        else if(!Input.GetKey(jumpKey))
        {
            jumpPressed = false;
            jumpTimer = 0;
        }
        else if(jumpPressed == true && jumpTimer < jumpTime)
        {
            jumpTimer += Time.deltaTime;
            myRb.drag = airDrag;
            myRb.velocity = (Vector2.up * jumpForce) + new Vector2(myRb.velocity.x, 0);
            jumpPressed = true;
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        //check for ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //set animators on ground
        myAnim.SetBool("OnGround", isGrounded);

        //horizontal movement
        if (Input.GetKeyDown(leftKey))
        {
            direction = -1;
        }
        else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            direction = -1;
        }
        else if (Input.GetKeyDown(rightKey))
        {
            direction = 1;
        }
        else if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
        {
            direction = 1;
        }
        else if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            direction = 0;
        }

        //animator settings
        if(direction == 0)
        {
            myAnim.SetBool("Moving", false);
        }
        else
        {
            myAnim.SetBool("Moving", true);
        }

        if (isGrounded && !jumpPressed)
        {
            myRb.drag = groundDrag;
            myRb.AddForce(new Vector2(direction * speed , 0));
        }
        else
        {
            myRb.drag = airDrag;
            myRb.AddForce(new Vector2(direction * airSpeed  , 0));
        }

        // Flip charcater
        if (direction != 0)
        {
            Vector3 Scaler = transform.localScale;
            Scaler.x = direction;
            transform.localScale = Scaler;
        }
    }
}
