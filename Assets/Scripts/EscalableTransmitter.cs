using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalableTransmitter : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform inicioCable, finCable;
    void Start()
    {
        transform.localPosition += Vector3.up*Ctrller.alturaToma*0.01f;
        GetComponentInChildren<LineRenderer>().SetPosition(0,inicioCable.position);
        GetComponentInChildren<LineRenderer>().SetPosition(1,finCable.position);
    }
}
