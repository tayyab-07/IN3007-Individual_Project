using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tutorial from: https://www.youtube.com/watch?v=f473C43s8nE&t=379s&ab_channel=Dave%2FGameDevelopment
// This file is NOT written by me

public class PlayerMovement : MonoBehaviour
{
    // headers to set variables in Unity 

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float initialCrouchYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // states to handle move speed
    public MovementState state;
    public enum MovementState
    { 
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        // freezes rotation on player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        // stores intitial y value of player to revert to after player is done crouching
        initialCrouchYScale = transform.localScale.y;
    }

    private void Update()
    {
        // performs a raycast to check if the player is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.01f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // adds ground drag only if player is on the floor
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        { 
            rb.drag = 0;
        }  
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        // gets keyboard WASD and arrow key inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // player jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            // jump
            Jump();

            // cooldown before resetting jump ability
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // player crouch
        if (Input.GetKeyDown(crouchKey))
        {
            // crouch shrinks the player to half thier size and puts them on the ground by using AddForce 
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // player un-crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, initialCrouchYScale, transform.localScale.z);
        }

    }

    private void StateHandler()
    {
        // different states changing move speed of player
        if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        if (grounded && Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        if (grounded && Input.GetKey(SprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        if (!grounded)
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // move speed on ground
        if (grounded)
        { 
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    
        // move speed in air using airMultiplier
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
            
    }

    // function to clamp move speed to desired max value
    private void SpeedControl()
    {
        // calculates move velocity
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // compares player velocity to max speed
        if (flatVel.magnitude > moveSpeed)
        {
            // limits velocity to max speed
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // jump by adding force in the upwards direction
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // funtion called when player is back on the ground
    private void ResetJump()
    {
        readyToJump = true;
    }
}
