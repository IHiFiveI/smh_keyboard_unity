using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float defaultSpeed = 12f;
    [SerializeField] private float runSpeed = 16f;
    private float currentSpeed = 12f;
    private float gravity = -19.62f;
    private float groundDistance = 0.4f;
    private float walkingTimer = 0.0f;
    Vector3 velocity = Vector3.zero;
    private bool isGrounded;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameObject animeLines;
    [SerializeField] private Animation SlideAnimation;
    [SerializeField] private Animation BodyAnimation;

    private float dashAnimationTimer = 0;
    private int dash = 0;
    private float dashTimer = 0.0f;
    public void Dash(int directon)
    {
        if (dashTimer > 3.0f)
        {
            dash = directon;
        }
        else
        {
            dash = 0;
        }
    }

    private void DashAnimation()
    {
        if (dashTimer > 3.0f && dash != 0)
        {
            if (dashAnimationTimer <= 0)
            {
                SlideAnimation.Play(((dash == 1) ? "SlideEffectLeft" : "SlideEffectRight"));
            }
            if (dashAnimationTimer < 0.3f)
            {
                controller.Move(transform.right * ((dash == 1) ? (-runSpeed * 2) : runSpeed * 2) * Time.unscaledDeltaTime);
                dashAnimationTimer += Time.unscaledDeltaTime;
            }
            else
            {
                dash = 0;
                dashTimer = 0;
                dashAnimationTimer = 0;
            }
        }
    }

    public void Move()
    {
        BodyAnimation.Play();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (z > 0)
        {
            walkingTimer += Time.unscaledDeltaTime;
        }
        else
        {
            walkingTimer = 0.0f;
            currentSpeed = defaultSpeed;
            animeLines.SetActive(false);
        }

        if (walkingTimer > 4.0f)
        {
            currentSpeed = runSpeed;
            animeLines.SetActive(true);
            walkingTimer = 0.0f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.unscaledDeltaTime);
    }

    private void Start()
    {
        animeLines.SetActive(false);
    }

    void Update()
    {
        Debug.Log("regular");
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        dashTimer += Time.unscaledDeltaTime;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            Time.timeScale = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Time.timeScale = 1.0f;
        }

        Move();

        DashAnimation();

        velocity.y += gravity * Time.unscaledDeltaTime;

        controller.Move(velocity * Time.unscaledDeltaTime);
    }
}
