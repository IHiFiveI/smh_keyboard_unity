using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float defaultSpeed = 12f;
    public float runSpeed = 16f;
    private float currentSpeed = 12f;
    public float gravity = -9.81f;
    public Transform groundChecker;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float timer = 0.0f;
    Vector3 velocity = Vector3.zero;
    bool isGrounded;
    public GameObject animeLines;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (z > 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
            currentSpeed = defaultSpeed;
            animeLines.SetActive(false);
        }

        if (timer > 7.0f)
        {
            currentSpeed = runSpeed;
            animeLines.SetActive(true);
            timer = 0.0f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
