using UnityEngine;
using System.Collections;

public class OppositeTrafficLightController : MonoBehaviour, ITrafficLight
{
    [Header("Lights")]
    public Renderer redLamp;
    public Renderer yellowLamp;
    public Renderer greenLamp;

    [Header("Materials")]
    public Material redOn;
    public Material yellowOn;
    public Material greenOn;
    public Material offMaterial;

    [Header("Light Timing")]
    public float redTime = 6f;
    public float yellowTime = 2f;
    public float greenTime = 6f;

    [HideInInspector]
    public bool isRed = false;

    public bool IsRed => isRed;

    void Start() => StartCoroutine(LightCycle());

    IEnumerator LightCycle()
    {
        while (true)
        {
            // GREEN for cars = pedestrians must STOP
            isRed = true;
            SetAllOff();
            greenLamp.material = greenOn;
            yield return new WaitForSeconds(greenTime);

            // YELLOW for cars = pedestrians must still STOP
            isRed = true;
            SetAllOff();
            yellowLamp.material = yellowOn;
            yield return new WaitForSeconds(yellowTime);

            // RED for cars = pedestrians can WALK
            isRed = false;
            SetAllOff();
            redLamp.material = redOn;
            yield return new WaitForSeconds(redTime);
        }
    }

    void SetAllOff()
    {
        redLamp.material = offMaterial;
        yellowLamp.material = offMaterial;
        greenLamp.material = offMaterial;
    }
}