using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour, ITrafficLight
{
    // References to the traffic light lamp objects
    [Header("Lights")]
    public Renderer redLamp;
    public Renderer yellowLamp;
    public Renderer greenLamp;

     // Materials used for ON and OFF states of the lamps
    [Header("Materials")]
    public Material redOn;
    public Material yellowOn;
    public Material greenOn;
    public Material offMaterial;

    [Header("Light Timing")]
    public float redTime = 6f;
    public float yellowTime = 2f;
    public float greenTime = 6f;

    // Indicates whether vehicles should stop
    [HideInInspector]
    public bool isRed = false;

    public bool IsRed => isRed;

    // Starts the traffic light cycle when the game begins.
    void Start() => StartCoroutine(LightCycle());

    // Continuously cycles through Red → Green → Yellow lights.
    // Updates the traffic light state and lamp materials.
    IEnumerator LightCycle()
    {
        while (true)
        {
            // Vehicles must stop, pedestrians can cross
            isRed = false;
            SetAllOff();
            redLamp.material = redOn;
            yield return new WaitForSeconds(redTime);

            // Vehicles can move, pedestrians must wait
            isRed = true;
            SetAllOff();
            greenLamp.material = greenOn;
            yield return new WaitForSeconds(greenTime);

            // Warning state before switching back to red
            isRed = true;
            SetAllOff();
            yellowLamp.material = yellowOn;
            yield return new WaitForSeconds(yellowTime);
        }
    }

    // Turns off all traffic light lamps by applying the OFF material.
    void SetAllOff()
    {
        redLamp.material = offMaterial;
        yellowLamp.material = offMaterial;
        greenLamp.material = offMaterial;
    }
}