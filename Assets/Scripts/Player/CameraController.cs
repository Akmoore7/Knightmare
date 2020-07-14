using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float minCameraX;
    public float maxCameraX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Camera follows the player with specified offset position between specified camera bounds (minCameraX, maxCameraX).
        transform.position = new Vector3(Mathf.Clamp(player.position.x, minCameraX, maxCameraX),
                player.position.y + offset.y,
                offset.z);

        //No Camera bound command (old)
        //transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); 
    }
}
