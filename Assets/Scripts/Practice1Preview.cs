using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Practice1Preview : MonoBehaviour
{
    public PracticeParameter heightParam, radiusParam, txHeightParam, pressureTakeParam;
    public TankResizer tank;
    public Transform pressurePoint, transmitter, txtPipeFinal;

    float tankHeight, tankRadius, txHeight, pressureTakeHeight;
    private void Awake() {
        OnValidate();
    }

    public void OnValidate() {
        tankHeight = heightParam.actualValue*0.01f;
        pressureTakeParam.max = tankHeight*100;
        pressureTakeParam.ValidateValue();

        tankRadius = radiusParam.actualValue*0.01f;
        txHeight = txHeightParam.actualValue*0.01f;
        pressureTakeHeight = pressureTakeParam.actualValue*0.01f;
        ValidateParameters();
    }

    public void SetParameters(float[] parameters)
    {
        tankHeight = parameters[0];
        tankRadius = parameters[1];
        txHeight = parameters[2];
        pressureTakeHeight = parameters[3];
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

        transmitter.gameObject.GetComponentInChildren<CableSpline>().OnValidate();
    }
}
