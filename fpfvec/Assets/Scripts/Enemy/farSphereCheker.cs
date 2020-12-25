using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farSphereCheker : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            enemy.GetComponent<playerDetection>().IsPlayerInVision = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            enemy.GetComponent<playerDetection>().IsPlayerInVision = false;
        }
    }
}
