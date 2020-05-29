using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform Player;

    private Vector3 startingPosition;
    private Vector3 endingPosition;

    public float doorOpenDistance = 7f;

    private bool doorClosed;

    void Start()
    {
        startingPosition = transform.position;
        endingPosition = startingPosition - new Vector3(0, 5f, 0);
        doorClosed = true;
    }

    void Update()
    {
        float dist = Vector3.Distance(Player.position, startingPosition);

        Debug.Log(dist);

        if (dist <= doorOpenDistance && doorClosed)
        {
            // Open door
            transform.position = endingPosition;
            doorClosed = false;
        } else if (dist >= doorOpenDistance && doorClosed == false)
        {
            // Close door
            transform.position = startingPosition;
            doorClosed = true;
        }
    }
}
