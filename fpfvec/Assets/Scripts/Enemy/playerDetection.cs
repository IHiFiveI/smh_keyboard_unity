using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{
    [SerializeField] private Collider firstSphere;
    [SerializeField] private Collider secondSphere;
    [SerializeField] private Transform head;
    private GameObject player;
    public bool IsPlayerInVision { get; set; }
    public bool IsPlayerClose { get; set; }
    private Transform defaultHeadPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defaultHeadPosition = head.transform;
    }

    void Update()
    {
        if (IsPlayerInVision)
        {
            Debug.Log("following");
            Vector3 playerDirection = (transform.position - player.transform.position).normalized;
            head.transform.rotation = Quaternion.LookRotation(playerDirection);
        }
        else
        {
            Debug.Log("not following");
            head.transform.rotation = defaultHeadPosition.rotation;
        }
    }
}
