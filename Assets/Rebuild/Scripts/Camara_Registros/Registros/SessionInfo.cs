using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionInfo : MonoBehaviour
{
    public TxtController DBRegister;

    public string _studentID;
    public int _sessionID = 0;
    public DateTime _sessionStartTime;
    public DateTime _sessionEndTime;

    private void Start()
    {
        DBRegister = GameObject.FindGameObjectWithTag("DBRegister").GetComponent<TxtController>();
    }
}
