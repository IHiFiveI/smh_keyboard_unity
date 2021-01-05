using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isReadyToRotate = false;
    Quaternion prevHeadPosition;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("Player Camera");
        defaultHeadRotation = head.transform.rotation.eulerAngles;
    }

    void Update()
    {
        Debug.DrawLine(head.position, head.position * 10, Color.green, 0.5f);
        if (IsPlayerInVision)
        {
            Vector3 playerDirection = (head.position - player.position).normalized;
            playerDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);
            head.transform.rotation = Quaternion.LookRotation(playerDirection);

            if (rotationTimer == 0.0f) prevHeadPosition = head.transform.rotation;
            rotationTimer += Time.deltaTime;

            if (rotationTimer > 0.5f && (Mathf.Abs(head.transform.rotation.eulerAngles.y - prevHeadPosition.eulerAngles.y) > 27))
            {
                isReadyToRotate = true;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, head.transform.rotation * Quaternion.AngleAxis(180.0f, Vector3.up), 1);
                Debug.Log(Mathf.Abs(head.transform.rotation.eulerAngles.y - prevHeadPosition.eulerAngles.y));
            }
            else
            {

                if (head.transform.localRotation.eulerAngles.y > 70.0f && head.transform.localRotation.eulerAngles.y < 290.0f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, head.transform.rotation * Quaternion.AngleAxis(180.0f, Vector3.up), 1);
                }
            }
            if (Mathf.Abs(transform.rotation.eulerAngles.y - head.transform.rotation.eulerAngles.y) == 0)
            {
                rotationTimer = 0.0f;
            }

        }
        else
        {
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, Quaternion.Euler(defaultHeadRotation), 1);
        }
    }
}
/*
        Debug.DrawLine(head.position, head.position * 10, Color.green, 0.5f);
        if (IsPlayerInVision)
        {
            // Debug.Log("following");

            Vector3 playerDirection = (head.position - player.position).normalized;
            playerDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);
            head.transform.rotation = Quaternion.LookRotation(playerDirection) * Quaternion.AngleAxis(-90.0f, Vector3.up);
            // Debug.DrawLine(player.position, player.position + playerDirection * 10, Color.red, 0.5f);
            Debug.Log(head.transform.localRotation.eulerAngles.y);
            Debug.Log(head.transform.rotation.eulerAngles.y);
            if (head.transform.localRotation.eulerAngles.y > 70.0f && head.transform.localRotation.eulerAngles.y < 300.0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(playerDirection), 1);
            }
        }
        else
        {
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, Quaternion.Euler(defaultHeadRotation), 1);
            // Debug.Log("not following");
        }
*/