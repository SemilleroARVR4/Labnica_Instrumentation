using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoltimeterScript : MonoBehaviour
{
    public bool m_InstrumentState = false;

    //Conectores.
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    [SerializeField] private GameObject mPantallaVoltimetro;

    //Text donde se visualiza el valor de voltaje.
    [SerializeField] private Text mTextVoltaje;

    [SerializeField] private TransmitterScript mTransmisor;

    public double  mVoltaje { get; set; } = 0;
    private double resistencia = 250;

    // Update is called once per frame
    void Update()
    {
        CheckConnections();
        if (m_InstrumentState)
        {
            mPantallaVoltimetro.SetActive(true);
            mVoltaje = mTransmisor.m_Corriente * resistencia;

            mVoltaje = System.Math.Round(mVoltaje, 2);

            if (m_PositiveConnector.iniTag == "positive" && m_NegativeConnector.iniTag == "negative")
            {
                mTextVoltaje.text = mVoltaje + " V";
            }
            else if (m_PositiveConnector.iniTag == "negative" && m_NegativeConnector.iniTag == "positive")
            {
                mTextVoltaje.text = -mVoltaje + " V";
            }
            else
            {
                mTextVoltaje.text = "0.0 V";
            }
        }
        else 
        {
            mPantallaVoltimetro.SetActive(true);
            mTextVoltaje.text = "0.0 V";
        }
    }
    void CheckConnections()
    {
        GameObject m_CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager");
        if (m_CircuitManager.GetComponent<CircuitManager>().m_ClosedCircuit)
            m_InstrumentState = true;
        else
            m_InstrumentState = false;
    }
}
