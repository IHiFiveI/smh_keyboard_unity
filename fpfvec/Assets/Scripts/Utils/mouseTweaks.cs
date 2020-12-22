using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseTweaks : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private const float DOUBLE_CLICK_TIME = 0.3f;
    private float lastClickTimeLeft;
    private float lastClickTimeRight;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTimeLeft < DOUBLE_CLICK_TIME)
            {
                player.GetComponent<playerMovement>().Dash(false);
            }
            lastClickTimeLeft = Time.time;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time - lastClickTimeRight < DOUBLE_CLICK_TIME)
            {
                player.GetComponent<playerMovement>().Dash(true);
            }
            lastClickTimeRight = Time.time;
        }
    }
}
