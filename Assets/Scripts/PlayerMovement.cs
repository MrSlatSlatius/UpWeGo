using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 3f;

    private CharacterController controller;
    private Vector3 move;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private float xMove;
    //Stun
    private float stunTime;
    private bool stuned;
    // Input delegates
    private Func<bool> jumpInput;
    private Func<float> moveInput;

    public bool Active { get; set; } = true;
    public Platform OnPlatform { get; private set; } = null;

    public void SetInputActions(Func<bool> jump, Func<float> horizontal)
    {
        jumpInput = jump;
        moveInput = horizontal;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        OnPlatform = hit.collider.GetComponent<Platform>();
    }

    private void Update()
    {
        if (!Active)
            return;
        if (!stuned)
        {
            xMove = moveInput.Invoke();
            if (jumpInput.Invoke() && isGrounded)
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        else
        {
            Stuned(0);
        }
    }

    void FixedUpdate()
    {
        UpdateGravity();
        UpdateColision();
        Move();
    }

    private void Move()
    {
        move = Active ? transform.right * xMove * speed : Vector3.zero;

        controller.Move((move + velocity) * Time.fixedDeltaTime);

        isGrounded = (controller.collisionFlags & CollisionFlags.CollidedBelow) == CollisionFlags.CollidedBelow;

        // Slow speed on-air
        if (!isGrounded)
            speed = currentSpeed / 1.5f;
        else
            speed = currentSpeed;
    }

    private void UpdateColision()
    {
        if (!isGrounded && velocity.y > 0f)
            Physics.IgnoreLayerCollision(9, 8);
        else if (Physics.Raycast(transform.position, -Vector3.up, 1.0f) && velocity.y < 0)
            Physics.IgnoreLayerCollision(9, 8, false);
    }

    private void UpdateGravity()
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -1f;
        if (!isGrounded)
            velocity.y += gravity * Time.fixedDeltaTime;
    }

    public void Stuned(float _stunTime)
    {
        if (stunTime == 0)
        {
            stuned = true;
            stunTime = _stunTime;
        }
        if (stunTime > 0)
        {
            stunTime -= Time.fixedDeltaTime;
        }
        else
        {
            stuned = false;
            stunTime = 0;
        }
    }
}
