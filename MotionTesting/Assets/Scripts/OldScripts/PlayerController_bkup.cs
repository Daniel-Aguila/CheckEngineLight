﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_bkup : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    public float jumpForce;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    //Occurs in step with the physics engine
    void FixedUpdate(){
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(moveInput > 0){
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0){
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Z) || isGrounded == true && Input.GetKeyDown(KeyCode.Slash)){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.Z) && isJumping == true || Input.GetKey(KeyCode.Slash) && isJumping == true){
            if(jumpTimeCounter > 0){
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else{
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Slash)){
            isJumping = false;
        }
    }
}
