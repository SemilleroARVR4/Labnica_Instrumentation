using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseVoltmeterScript : MonoBehaviour
{
    public bool m_InstrumentState = false;

    //Conectores.
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    [SerializeField] private GameObject mPantallaVoltimetro;

    //Text donde se visualiza el valor de voltaje.
    [SerializeField] private Text mTextVoltaje;


    public double mVoltaje { get; set; } = 0;
    public double mCorriente { get; set; } = 0;

    private double resistencia = 250;

    // Update is called once per frame
    void Update()
    {
        m_InstrumentState = CheckConnections();
        mVoltaje = CalculateVoltage(mCorriente,resistencia);
        CanvasBehavior();
    }

    bool CheckConnections()
    {
        GameObject m_CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager");
        if (m_CircuitManager.GetComponent<CircuitManager>().m_ClosedCircuit)
            return true;
        else
            return false;
    }

    double CalculateVoltage(double mCorri,double mRes) 
    {
        double voltage;
        voltage = mCorri * mRes;
        voltage = System.Math.Round(voltage, 2);
        return voltage;
    }

    void CanvasBehavior() 
    {
        if (m_InstrumentState)
        {
            mPantallaVoltimetro.SetActive(true);

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


}
