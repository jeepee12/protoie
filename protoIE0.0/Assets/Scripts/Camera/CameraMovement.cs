﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
        /*if (player.GetComponent<PlayerMovement>())
            player.GetComponent<PlayerMovement>().movement;*/
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}