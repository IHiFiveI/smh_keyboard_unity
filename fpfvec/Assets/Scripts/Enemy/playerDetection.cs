using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerDetection : MonoBehaviour
{
    [SerializeField] private Collider firstSphere;
    [SerializeField] private Collider secondSphere;
    [SerializeField] private Transform head;
    private Transform player;
    public bool IsPlayerInVision { get; set; }
    public bool IsPlayerClose { get; set; }
    private Vector3 defaultHeadRotation;
    private float rotationTimer = 0.0f;
    Quaternion prevHeadPosition;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("Player Camera");
        defaultHeadRotation = head.transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (IsPlayerInVision)
        {
            Vector3 playerDirection = (head.position - player.position).normalized;
            playerDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, Quaternion.LookRotation(playerDirection), Time.deltaTime * 360);
        }
        else
        {
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, Quaternion.Euler(defaultHeadRotation), Time.deltaTime * 100);
        }

        if (rotationTimer == 0.0f) prevHeadPosition = head.transform.rotation;
        rotationTimer += Time.deltaTime;

        if ((rotationTimer > 0.5f && (Mathf.Abs(head.transform.rotation.eulerAngles.y - prevHeadPosition.eulerAngles.y) > 27)) ||
            (head.transform.localRotation.eulerAngles.y > 70.0f && head.transform.localRotation.eulerAngles.y < 290.0f) || rotationTimer > 3.0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, head.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * Quaternion.AngleAxis(180.0f, Vector3.up), Time.deltaTime * 100);
        }
        if (Mathf.Abs(transform.rotation.eulerAngles.y - head.transform.rotation.eulerAngles.y) == 0)
        {
            rotationTimer = 0.0f;
        }
    }
}