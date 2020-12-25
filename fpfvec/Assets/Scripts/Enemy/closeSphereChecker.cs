using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeSphereChecker : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            enemy.GetComponent<playerDetection>().IsPlayerClose = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            enemy.GetComponent<playerDetection>().IsPlayerClose = false;
        }
    }
}
