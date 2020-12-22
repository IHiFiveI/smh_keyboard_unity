using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private ParticleSystem dashEffectRight;
    [SerializeField] private ParticleSystem dashEffectLeft;
    private float dashTimer = 0.0f;
    public void Dash(bool directon)
    {
        if (dashTimer > 3.0f)
        {
            if (directon)
            {
                dashEffectRight.Play();
                controller.Move(transform.right * currentSpeed * 200 * Time.deltaTime);
            }
            else
            {
                dashEffectLeft.Play();
                controller.Move(transform.right * currentSpeed * -200 * Time.deltaTime);
            }
            dashTimer = 0.0f;
            dashEffectLeft.Pause();
            dashEffectRight.Pause();
        }
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (z > 0)
        {
            walkingTimer += Time.deltaTime;
        }
        else
        {
            walkingTimer = 0.0f;
            currentSpeed = defaultSpeed;
            animeLines.SetActive(false);
        }

        if (walkingTimer > 7.0f)
        {
            currentSpeed = runSpeed;
            animeLines.SetActive(true);
            walkingTimer = 0.0f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
    void Start()
    {
        dashEffectLeft.Pause();
        dashEffectRight.Pause();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        dashTimer += Time.deltaTime;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move();

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
