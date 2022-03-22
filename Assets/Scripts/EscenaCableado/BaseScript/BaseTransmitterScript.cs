using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTransmitterScript : MonoBehaviour
{
    public bool m_InstrumentState = false;

    //Rango maximo y minimo del transmisor que sera ajustado por el handterminal.
    private double m_RangoMax = 3;
    private double m_RangoMin = 0;

    public double m_Presion { get; set; }
    public double m_Corriente { get; set; } = 0;

    //Conectores.
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    [SerializeField] private GameObject mPantallaTransmitter;

    //Cuadro de texto del transmisor donde se visualiza el valor de presión.
    [SerializeField] private Text m_TextPresion;
    [SerializeField] private Tank m_Tanque;
    [SerializeField] private HandTerminal m_HandTerminal;


    // Update is called once per frame
    void Update()
    {
        CheckConnections();
        if (m_InstrumentState)
        {
            mPantallaTransmitter.SetActive(true);
            m_Presion = m_Tanque.m_Presion; //0-3psi
            m_Presion = System.Math.Round(m_Presion, 2);

            m_RangoMax = m_HandTerminal.mRangoMax;
            m_RangoMin = m_HandTerminal.mRangoMin;

            //Detalles relacionados con los valores maximos y minimos que puede leer el transmisor.
            if (m_Presion < m_RangoMin)
            {
                m_Presion = 0.9 * m_RangoMin;
            }
            else if (m_Presion > m_RangoMax)
            {
                m_Presion = 1.1 * m_RangoMax;
            }

            //Despliegue de la presión en un text
            m_TextPresion.text = m_Presion.ToString();

            //Calculo de la corriente y aproximación.
            m_Corriente = 4 * Mathf.Pow(10, -3) + (m_Presion - m_RangoMin) * (((16 * Mathf.Pow(10, -3))) / (m_RangoMax - m_RangoMin));
            m_Corriente = System.Math.Round(m_Corriente, 5);

            //Detalles relacionados con los rangos maximos y minimos que puede leer el transmisor.
            if (m_Corriente > 0.021)
            {
                m_Corriente = 0.021;
            }
            else if (m_Corriente < 0.004)
            {
                m_Corriente = 0.00393;
            }
        }
        else
        {
            mPantallaTransmitter.SetActive(false);
        }
    }

    void CheckConnections()
    {
        GameObject m_CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager");
        if (m_PositiveConnector.iniTag == "positive" && m_NegativeConnector.iniTag == "negative" && m_CircuitManager.GetComponent<CircuitManager>().m_ClosedCircuit)
            m_InstrumentState = true;
        else
            m_InstrumentState = false;
    }
}

