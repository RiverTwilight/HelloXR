using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 currentVelocity;

    public Camera FPcamera;

    public ItemManager itemManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        HandleUseItem();
    }

    public void HandleUseItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Use item
            Debug.Log("Use item");
        }
    }

    public void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = FPcamera.transform.forward;
        Vector3 cameraRight = FPcamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraForward * moveVertical + cameraRight * moveHorizontal;

        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundMask);

        if (!isGrounded)
        {
            // Calculate the target velocity for smooth damping
            Vector3 targetVelocity = movement * speed;

            // Smoothly damp the object's current velocity towards the target velocity
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, 0.1f);

            // Jump if the player presses the space bar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

}

