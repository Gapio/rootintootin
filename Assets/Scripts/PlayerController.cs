using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float movespeed = 10f;

    private float xInput;
    private float yInput;

    void Awake() { 
        rb = GetComponent<Rigidbody>();
    }

// Update is called once per frame
void Update() //Animations and inputs
    {
        ProcessInputs();
    }
    private void FixedUpdate() // Movement
    {
        Move();
    }
    private void ProcessInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }
    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0f, yInput) * movespeed);
    }
}
