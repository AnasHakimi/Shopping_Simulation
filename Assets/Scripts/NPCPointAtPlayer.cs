using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPointAtPlayer : MonoBehaviour
{
    public Transform player; // Assign the player's Transform in the Inspector
    public float rotationSpeed = 5f; // Speed at which the NPC rotates towards the player

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector3 direction = player.position - transform.position;

            // Keep the NPC upright (ignore Y-axis differences)
            direction.y = 0;

            // Check if the direction vector is not zero (to avoid errors)
            if (direction != Vector3.zero)
            {
                // Smoothly rotate the NPC to look at the player
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
