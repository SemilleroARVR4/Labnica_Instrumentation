using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckInfo : MonoBehaviour
{
    public TxtController DBRegister;

    public int _sessionID;
    public int _checkID = 0;
    public DateTime _checkTime;
    public bool _closedLoop = false;

    private void Start()
    {
        DBRegister = GameObject.FindGameObjectWithTag("DBRegister").GetComponent<TxtController>();
    }
}
