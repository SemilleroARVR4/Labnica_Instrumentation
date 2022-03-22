using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TankResizable : MonoBehaviour
{
    public int alturaCm;
    public Transform heigtBone;

    public void Start() {
        alturaCm = Ctrller.alturaTanque;
    }

    private void Update() {
        if(heigtBone.transform.localPosition.z != alturaCm*0.01f)
            heigtBone.transform.localPosition = Vector3.forward*alturaCm*0.01f;
    }
}
