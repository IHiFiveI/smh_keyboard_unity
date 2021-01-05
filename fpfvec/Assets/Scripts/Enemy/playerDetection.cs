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
            Debug.Log("following");

            Vector3 playerDirection = (head.position - player.position).normalized;
            playerDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);
            head.transform.localRotation = Quaternion.LookRotation(playerDirection);

            Debug.DrawLine(player.position, player.position + playerDirection * 10, Color.red, 0.5f);
        }
        else
        {
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, Quaternion.Euler(defaultHeadRotation), 1);
            Debug.Log("not following");
        }
    }
}
