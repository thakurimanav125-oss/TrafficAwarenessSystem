using UnityEngine;

// Controls pedestrian movement between predefined waypoints.
// The pedestrian moves continuously along the path and loops
// back to the first waypoint after reaching the last one.
public class PedestrianMovement : MonoBehaviour
{
    // List of waypoint positions that define the walking path
    public Transform[] waypoints;
    int index = 0;

    public float speed = 2f;

    // Minimum distance required to consider a waypoint reached
    public float reachDistance = 0.2f;

    public bool canMove = true;

    // Called every frame.
    // Moves the pedestrian only if movement is enabled and at least one waypoint exists.
    void Update()
    {
        if (!canMove || waypoints.Length == 0)
            return;

        Move();
    }

    void Move()
    {   
        // Get the current destination waypoint
        Transform target = waypoints[index];

        //calculates the direction from the pedestrian's current position to the target waypoint and converts it into a unit vector.
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
            transform.forward = dir;

        // Check if the waypoint has been reached
        if (Vector3.Distance(transform.position, target.position) < reachDistance)
        {
            index++;
            // Restart from the first waypoint after reaching the last one
            if (index >= waypoints.Length)
                index = 0;
        }
    }
}