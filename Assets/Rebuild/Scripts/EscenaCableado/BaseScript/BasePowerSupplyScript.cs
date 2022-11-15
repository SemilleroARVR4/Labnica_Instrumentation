using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BasePowerSupplyScript : MonoBehaviour
{
    public float m_supply_voltage;
    public bool m_InstrumentState = false;
    public bool m_AlreadyCheck = false;
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    // Start is called before the first frame update
    void Start()
    {
        m_PositiveConnector.iniTag = "positive";
        m_NegativeConnector.iniTag = "negative";
    }

    public void TurnOnCircuit()
    {
        m_InstrumentState = true;

        //Revisamos que los cables que no esten conectados sean eliminados de la escena.
        GameObject[] cableList = GameObject.FindGameObjectsWithTag("Cable");
        foreach (GameObject cable in cableList)
        {
            if (!cable.transform.GetChild(0).GetComponent<ConnectCable>().DisconnectCanvas.activeSelf || !cable.transform.GetChild(1).GetComponent<ConnectCable>().DisconnectCanvas.activeSelf)
            {
                Destroy(cable);
            }
        }

        //Ejecutamos la revision de tags en cada conexion a la fuente.
        foreach (GameObject conexion in m_PositiveConnector.endGameObjects)
        {
            if (conexion == null)
                continue;
            else
            {
                m_PositiveConnector.m_AlreadyCheck = true;
                conexion.GetComponent<ConnectConnector>().ConnectCircuit(m_PositiveConnector.gameObject);
            }
        }

        //Codigo para registrar el chequeo de cables
        GameObject _DBRegister = GameObject.FindGameObjectWithTag("DBRegister");
        GameObject _CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager");

        //Registramos la infromación del chequeo
        _DBRegister.GetComponent<CheckInfo>()._checkTime = System.DateTime.Now;
        _DBRegister.GetComponent<CheckInfo>()._closedLoop = _CircuitManager.GetComponent<CircuitManager>().m_ClosedCircuit;

        _DBRegister.GetComponent<TxtController>().WriteCheckInfo();

        _DBRegister.GetComponent<CheckInfo>()._checkID += 1;

        //Registramos todos los cables que quedaron activos al chequear.
        foreach (GameObject cable in cableList)
        {
            _DBRegister.GetComponent<TxtController>().WriteCableInfo(cable.GetComponent<CableInfo>());

            cable.GetComponent<CableInfo>()._sessionID = _DBRegister.GetComponent<CheckInfo>()._sessionID;
            cable.GetComponent<CableInfo>()._checkID = _DBRegister.GetComponent<CheckInfo>()._checkID;
        }
    }
}
