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

    private Animator anim;
    private CharacterController controller;
    private Vector3 move;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private float xMove;
    
    // Input delegates
    private Func<bool> jumpInput;
    private Func<float> moveInput;
    private bool applyGravity = true;

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
        anim = GetComponent<Animator>();
        currentSpeed = speed;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        OnPlatform = hit.collider.GetComponent<Platform>();
    }

    private void Update()
    {
        xMove = moveInput.Invoke();

        if (!Active)
            return;

        if (jumpInput.Invoke() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void FixedUpdate()
    {
        if (ApplyGravity)
            UpdateGravity();
        UpdateColision();
        UpdateAnimations();
        Move();
    }

    private void UpdateAnimations()
    {
        anim.SetFloat("xSpeed", Math.Abs(xMove));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", velocity.y);
    }

    private void Move()
    {
        move = Active ? transform.right * xMove * speed : Vector3.zero;

        controller.Move((move + velocity + Motion) * Time.fixedDeltaTime);

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

    public void SetInputActions(Func<bool> jump, Func<float> horizontal)
    {
        jumpInput = jump;
        moveInput = horizontal;
    }
}
