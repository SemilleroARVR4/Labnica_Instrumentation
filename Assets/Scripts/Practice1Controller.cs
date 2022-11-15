using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice1Controller : MonoBehaviour
{
    public TankResizer tank;
    public Transform pressurePoint, transmitter, txtPipeFinal;

    public float tankHeight, tankRadius, txHeight, pressureTakeHeight, density;


    private void Start() {
        SetParameters(Practice1Parameters.parameters);
    }

    public void SetParameters(float [] parameters)
    {
        tankHeight = parameters[0]*0.01f;
        tankRadius = parameters[1]*0.01f;
        txHeight = parameters[2]*0.01f;
        pressureTakeHeight = parameters[3]*0.01f;
        density = parameters[4];
        ValidateParameters();
    }

    public void ValidateParameters()
    {
        tank.radius = tankRadius;
        tank.height = tankHeight;
        tank.OnValidate();

        if(txHeight<-0.5f)
            tank.legHeight = -(txHeight-0.2f);
        else
            tank.legHeight = 0.7f;

        var txStartPos = pressurePoint.position + pressurePoint.forward*0.6f;
        txStartPos.y = tank.tankBase.position.y + txHeight;
        transmitter.position = txStartPos;
        txtPipeFinal.position = pressurePoint.position + Vector3.up*pressureTakeHeight;

        foreach(var obj in FindObjectsOfType<AttatchedObject>())
            obj.AdjustCable();
        foreach(var cable in FindObjectsOfType<CableSpline>())
            cable.OnValidate();
    }
}
