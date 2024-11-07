using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 5f;      // Speed of the ship
    public Transform shipTransform;   // Reference to the ship's transform
    public InfinitSea sea;           // Reference to the InfiniteSea script

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = shipTransform.position;  // Initialize target position
    }

    void Update()
    {
        // Handle mouse click to set new target position
        if (Input.GetMouseButtonDown(0))  // Left mouse button
        {
            SetTargetPosition();
        }

        // Move the ship if it's set to move
        if (isMoving)
        {
            MoveShip();
            sea.UpdateTilePositions();  // Update sea tiles around the player
        }
    }

    void SetTargetPosition()
    {
        // Raycast from the mouse position to the plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;  // Set target to clicked position
            isMoving = true;             // Start moving the ship
        }
    }

    void MoveShip()
    {
        // Move the ship toward the target position
        shipTransform.position = Vector3.MoveTowards(shipTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the ship has reached the target
        if (Vector3.Distance(shipTransform.position, targetPosition) < 0.1f)
        {
            isMoving = false;  // Stop moving when the ship reaches the target
        }

        // Face the target position
        Vector3 direction = (targetPosition - shipTransform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }
    }
}
