using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float movespeed = 10f;
    public float jumpHeight;

    private float xInput;
    private float yInput;

    [Header("References")]
    public Transform Orientation;
    public Transform playerCam;
    // Private Rigidbody
    //private PlayerMovementDashing pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    public float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() //Animations and inputs
    {
        ProcessInputs();
        if (Input.GetKeyDown(dashKey))
            Dash();
    }
    private void FixedUpdate() // Movement
    {
        Move();
        
    }
    private void ProcessInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(xInput, jumpHeight, yInput) * jumpHeight);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            rb.AddForce(new Vector3(xInput, jumpHeight, yInput) * jumpHeight);
        }
    }
    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0f, yInput) * movespeed);
    }
    private void Dash()
    {
        Vector3 ForceforceToApply = Orientation.forward * dashForce + Orientation.up * dashUpwardForce;
        rb.AddForce(ForceforceToApply, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashDuration);
    }
    private void ResetDash()
    {

    }
}