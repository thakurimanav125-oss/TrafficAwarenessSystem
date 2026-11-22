
using UnityEngine;
using System.Collections.Generic;

public class PedStopTrigger : MonoBehaviour
{
    [Header("Assign S/N OR E/W Traffic Light Controller")]
    public MonoBehaviour trafficLightController;

    private ITrafficLight lightRef;
    private List<PedestrianMovement> waitingPeds = new List<PedestrianMovement>();

    void Start()
    {
        if (trafficLightController != null)
        {
            // Try direct cast first
            lightRef = trafficLightController as ITrafficLight;

            // If failed, search in children
            if (lightRef == null)
                lightRef = trafficLightController.GetComponentInChildren<ITrafficLight>();

            // If still failed, search in parent
            if (lightRef == null)
                lightRef = trafficLightController.GetComponentInParent<ITrafficLight>();
        }

        // Final check
        if (lightRef == null)
            Debug.LogError($"{gameObject.name}: Could not find ITrafficLight! Check assignment.");
        else
            Debug.Log($"{gameObject.name}: lightRef found → {lightRef}"); // ← Should print
    }

    void Update()
    {
        if (lightRef == null) return;

        foreach (var ped in waitingPeds)
        {
            if (ped == null) continue;
            ped.canMove = !lightRef.IsRed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pedestrian")) return;

        PedestrianMovement ped = other.GetComponent<PedestrianMovement>();
        if (ped == null) return;

        if (!waitingPeds.Contains(ped))
            waitingPeds.Add(ped);

        if (lightRef != null && lightRef.IsRed)
            ped.canMove = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Pedestrian")) return;

        PedestrianMovement ped = other.GetComponent<PedestrianMovement>();
        if (ped == null) return;

        waitingPeds.Remove(ped);
        ped.canMove = true;
    }
}