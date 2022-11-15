using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    public GameObject _powerSupply;

    public bool m_ClosedCircuit = false;

    public Light _startLight;
    public Light _stopLight;

    // Update is called once per frame
    void Update()
    {
        if (_powerSupply.GetComponent<PowerSupplyScript>().m_InstrumentState)
        {
            _startLight.enabled = true;
            _stopLight.enabled = false;
        }
        else
        {
            _startLight.enabled = false;
            _stopLight.enabled = true;
        }
    }

    public void ResetCircuit()
    {
        ResetCircuit(false);
    }

    public void ResetCircuit(bool remote)
    {
        //Mp Callback
        if(!remote)
            FindObjectOfType<MPGameMgr>().photonView.RPC("PowerOff",Photon.Pun.RpcTarget.Others);

        _powerSupply.GetComponent<PowerSupplyScript>().m_InstrumentState = false;

        //Resetear todos los conectores
        ConnectConnector[] ConnectorList;

        ConnectorList = GameObject.FindObjectsOfType<ConnectConnector>();

        foreach (ConnectConnector connector in ConnectorList)
        {
            //Debug.Log(connector.name);

            connector.iniTag = "";

            if (connector.transform.parent.transform.parent.CompareTag("PowerSupply") && connector.gameObject.name == "Connector+")
                connector.iniTag = "Positive";
            if (connector.transform.parent.transform.parent.CompareTag("PowerSupply") && connector.gameObject.name == "Connector-")
                connector.iniTag = "Negative";

            connector.m_AlreadyCheck = false;
        }

        //Abrir el lazo
        m_ClosedCircuit = false;

        //Limpia el debug
        //Para el ejecutable se debe comentar este codigo.
        /*
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
        */
    }
}
