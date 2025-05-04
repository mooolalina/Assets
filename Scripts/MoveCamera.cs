using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Transform player;
    public float camPositionSpeed = 5f; 
    public Vector3 offset; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() 
    {
        Vector3 newCamPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, newCamPosition, camPositionSpeed * Time.deltaTime);
    }
}
