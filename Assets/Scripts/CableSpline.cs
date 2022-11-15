using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
public class CableSpline : MonoBehaviour
{
    public Transform start, end;
    public float diameter = 5;
    public float startRoll;
    public float straightLenght;
    Spline spline;
    public bool udpater;

    public void OnValidate() {
        spline = GetComponentInChildren<Spline>();
        GetComponentInChildren<SplineMeshTiling>().scale = Vector3.one*diameter*0.01f;
        float dir = -0.1f;
        if(start.localPosition.z<end.localPosition.z)
            dir = 0.1f;
        dir *= straightLenght;
        var nodeStart = new SplineNode(start.localPosition, start.localPosition - start.forward*dir);
        var nodeEnd= new SplineNode(end.localPosition, end.localPosition + end.forward *dir);

        nodeStart.Roll = startRoll;
        spline.nodes[0] = nodeStart;
        spline.nodes[1] = nodeEnd;
        spline.RefreshCurves();
    }
}
