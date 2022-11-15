using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmperimeterScript : MonoBehaviour
{
    public bool m_InstrumentState = false;
    public double mCorriente { get; set; }

    //Conectores.
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    [SerializeField] private GameObject mPantallaAmp;
    [SerializeField] private Text mTextCorriente;
    [SerializeField] private TransmitterScript mTransmisor;

    // Update is called once per frame
    void Update()
    {
        CheckConnections();
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

    void CheckConnections()
    {
        GameObject m_CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager");
        if (((m_PositiveConnector.iniTag == "positive" && m_NegativeConnector.iniTag == "negative") || (m_PositiveConnector.iniTag == "negative" && m_NegativeConnector.iniTag == "positive")) && m_CircuitManager.GetComponent<CircuitManager>().m_ClosedCircuit)
            m_InstrumentState = true;
        else
            m_InstrumentState = false;
    }
}
