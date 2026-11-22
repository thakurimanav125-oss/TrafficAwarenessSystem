using UnityEngine;

// Cars stop or move based on the current traffic light state.
public class TrafficStopTrigger : MonoBehaviour
{
    // Reference to the traffic light controller
    public TrafficLightController trafficLight;

    // Updates the car's movement permission according to the traffic light.
    private void OnTriggerStay(Collider other)
    {
        // Check if the object inside the trigger is a car
        if (other.CompareTag("Car"))
        {
            // Get the CarMovement component from the detected car
            CarMovement car = other.GetComponent<CarMovement>();

            if (car != null)
            {
                // isRed=true means GREEN for cars (peds stop)
                // isRed=false means RED for cars (peds walk)
                car.canMove = trafficLight.isRed; // ← flipped
            }
        }
    }

    // Called when a car exits the trigger area.
    // Allows the car to continue moving normally.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
             // Get the CarMovement component from the detected car
            CarMovement car = other.GetComponent<CarMovement>();

            // Re-enable movement after leaving the traffic control zone
            if (car != null)
                car.canMove = true;
        }
    }
}