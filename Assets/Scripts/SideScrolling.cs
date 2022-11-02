using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

     void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

     void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = Mathf.Max(cameraPos.x, player.position.x);
        transform.position = cameraPos;
    }
}
