using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMGR : MonoBehaviour
{
    public GameObject circuitDiagram, controlsImg;
    public void ToggleCircuit()
    {
        circuitDiagram.SetActive(!circuitDiagram.activeSelf);
    }
    public void ToggleControls()
    {
        controlsImg.SetActive(!controlsImg.activeSelf);
    }
}
