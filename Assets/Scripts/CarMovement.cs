using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Car Settings")]
    public float speed = 5f;
    public float rotationSpeed = 5f;

    // It adds a labeled heading in the Unity Inspector above variables.
    [Header("Waypoints")]
    public Transform[] waypoints;

    [HideInInspector]
    public bool canMove = true;

    private int currentWaypoint = 0;

    void Update()
    {
        // Checks whether the vehicle is allowed to move.
        // STOP if red light says stop
        if (!canMove)
            return;

        // Safety check
        if (waypoints.Length == 0)
            return;

        // Current target waypoint
        Transform targetWaypoint = waypoints[currentWaypoint];

        // Direction to waypoint
        Vector3 direction = targetWaypoint.position - transform.position;

        // Ignore height
        direction.y = 0;

        // Distance
        float distance = direction.magnitude;

        // Normalize direction
        Vector3 moveDirection = direction.normalized;

        // Move forward
        transform.position += moveDirection * speed * Time.deltaTime;

        // Rotate car smoothly toward movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Check if car is close enough to waypoint
        if (distance < 1f)
        {
            currentWaypoint++;

            // Loop back to start
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}