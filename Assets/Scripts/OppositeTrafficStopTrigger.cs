using UnityEngine;

public class OppositeTrafficStopTrigger : MonoBehaviour
{
    public OppositeTrafficLightController trafficLight;

    private void OnTriggerStay(Collider other)
    {
        if (trafficLight == null)
        {
            Debug.LogError($"{gameObject.name}: trafficLight NOT assigned in Inspector!");
            return;
        }

        if (other.CompareTag("Car"))
        {
            CarMovement car = other.GetComponent<CarMovement>();
            if (car != null)
                car.canMove = trafficLight.isRed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            CarMovement car = other.GetComponent<CarMovement>();
            if (car != null)
                car.canMove = true;
        }
    }
}