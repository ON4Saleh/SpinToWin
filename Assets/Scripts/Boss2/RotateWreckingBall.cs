using System;
using UnityEngine;

public class RotateWreckingBall : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform; // Reference to the player's transform

    [SerializeField]
    private float rotationSpeed = 10f; // Speed of rotation

    [SerializeField]
    private float rotationThreshold = 5f; // Threshold to consider rotation complete

    private bool isRotatedTowardPlayer = false;

    public bool IsRotatedTowardPlayer => isRotatedTowardPlayer;

   

    void FixedUpdate()
    {

        RotateToPlayer();
    }

   

    public void RotateToPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        // Calculate the direction to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Optional: Keep the rotation only on the Y axis

        // Create a rotation that looks at the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the rotation is complete
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        isRotatedTowardPlayer = angleDifference <= rotationThreshold;
    }
}