﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 3f;
    private CharacterController controller;
    [SerializeField]
    private float drag = 1.5f;
    private Vector3 move;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private bool applyGravity = true;
    private Vector3 force = Vector3.zero;

    public Vector3 Motion { get; set; } = Vector3.zero;
    public bool ApplyGravity
    {
        get => applyGravity;

        set
        {
            applyGravity = value;
            velocity = Vector3.zero;
        }
    }
    public bool Active { get; set; } = true;
    public Platform OnPlatform { get; private set; } = null;
    public CharacterController Controller => controller;

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

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            OnPlatform = null;
        }
    }

    void FixedUpdate()
    {
<<<<<<< HEAD:Assets/Scritps/PlayerMovement.cs
        force = Vector3.MoveTowards(force, Vector3.zero,
            Time.fixedDeltaTime * drag);
        if (force.x > 0)
        {
            force.x -= Mathf.Round(force.x * 0.15f);
            if (force.x < 0) force.x = 0;
        }

=======
>>>>>>> origin/master:Assets/Scripts/PlayerMovement.cs
        if (ApplyGravity)
            UpdateGravity();
        UpdateColision();
        Move();
    }

    private void Move()
    {
        //Input check
        float xMove = Input.GetAxis("Horizontal");

        move = Active ? transform.right * xMove * speed : Vector3.zero;

<<<<<<< HEAD:Assets/Scritps/PlayerMovement.cs
        controller.Move((move + velocity + Motion + force) * Time.fixedDeltaTime);
=======
        controller.Move((move + velocity + Motion) * Time.fixedDeltaTime);
>>>>>>> origin/master:Assets/Scripts/PlayerMovement.cs

        isGrounded = (controller.collisionFlags & CollisionFlags.CollidedBelow) == CollisionFlags.CollidedBelow;

        // Slow speed on-air
        if (!isGrounded)
            speed = currentSpeed / 1.5f;
        else
            speed = currentSpeed;

        Motion = Vector3.zero;
    }

    private void UpdateColision()
    {
        if (!isGrounded && controller.velocity.y > 0f)
            Physics.IgnoreLayerCollision(9, 8);
        else if (Physics.Raycast(transform.position, -Vector3.up, 1.0f) && velocity.y < 0)
            Physics.IgnoreLayerCollision(9, 8, false);
    }

    public void Move(Vector3 motion)
    {
        Motion = motion;
    }

    private void UpdateGravity()
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -1f;
        if (!isGrounded)
            velocity.y += gravity * Time.fixedDeltaTime;
    }

    public void AddForce(Vector3 force)
    {
        this.force = force;
    }
}
