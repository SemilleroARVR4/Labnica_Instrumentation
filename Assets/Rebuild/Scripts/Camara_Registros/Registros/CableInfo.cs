using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CableInfo : MonoBehaviour
{
    public TxtController DBRegister;

    public int _sessionID = 0;
    public int _checkID = 0;
    public int _cableID = 0;
    public DateTime _startTimeCable = DateTime.MinValue;
    public DateTime _deleteTimeCable = DateTime.MinValue;
    public string _deviceTouched1 = null;
    public string _deviceTouched2 = null;
    public string _connectorTouched1 = null;
    public string _connectorTouched2 = null;
    public string _cableState = null; //activated, deleted.

    private void Start()
    {
        DBRegister = GameObject.FindGameObjectWithTag("DBRegister").GetComponent<TxtController>();
    }
}
