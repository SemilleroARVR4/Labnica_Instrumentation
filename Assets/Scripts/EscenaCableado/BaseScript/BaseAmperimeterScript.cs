using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseAmperimeterScript : MonoBehaviour
{
    public bool m_InstrumentState = false;
    public double mCorriente { get; set; }

    //Conectores.
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    [SerializeField] private GameObject mPantallaAmp; //Canvas del amperimetro
    [SerializeField] private Text mTextCorriente; //Texto donde visualizo mi corriente
    [SerializeField] private TransmitterScript mTransmisor;

    // Update is called once per frame
    void Update()
    {
        m_InstrumentState = CheckConnections();

        //Falta agregar asignación de la corriente.

        CanvasBehavior();
    }

    bool CheckConnections()
    {
        GameObject m_CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager");
        if (((m_PositiveConnector.iniTag == "positive" && m_NegativeConnector.iniTag == "negative") || (m_PositiveConnector.iniTag == "negative" && m_NegativeConnector.iniTag == "positive")) && m_CircuitManager.GetComponent<CircuitManager>().m_ClosedCircuit)
            return true;
        else
            return false;
    }

    void CanvasBehavior()
    {
        if (m_InstrumentState)
        {
            mPantallaAmp.SetActive(true);
            mCorriente = mTransmisor.m_Corriente;
            if (m_PositiveConnector.iniTag == "positive" && m_NegativeConnector.iniTag == "negative")
            {
                mTextCorriente.text = (mCorriente * 1000) + " mA";
            }
            if (m_PositiveConnector.iniTag == "negative" && m_NegativeConnector.iniTag == "positive")
            {
                mTextCorriente.text = -(mCorriente * 1000) + " mA";
            }
        }
        else
        {
            mPantallaAmp.SetActive(true);
            mTextCorriente.text = "0.0 mA";
        }
    }



}
