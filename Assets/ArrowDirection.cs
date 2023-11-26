using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform arrow; // Reference to the arrow

    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        // Initialize lastPosition to the player's starting position
        lastPosition = player.transform.position;
    }

    void Update()
    {
        // Calculate the current velocity
        currentVelocity = (player.transform.position - lastPosition) / Time.deltaTime;

        // Update lastPosition for the next frame
        lastPosition = player.transform.position;
        // Get the player's velocity

        print(currentVelocity);

        // If the player is moving
        if (currentVelocity != Vector3.zero)
        {
            // Rotate the arrow to point in the direction of the player's movement
            arrow.rotation = Quaternion.LookRotation(currentVelocity);
        }
        //else
        //{
        //    arrow.gameObject.SetActive(false);
        //}
    }
}
