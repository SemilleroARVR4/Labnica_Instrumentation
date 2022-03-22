using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class HandTerminal : MonoBehaviourPunCallbacks
{
    public bool m_InstrumentState = false;

    //Condicion de ON/OFF relacionada con el boton de ON/OFF del handTerminal.
    private bool mCondOnOff = false; 

    //Conectores.
    public ConnectConnector m_PositiveConnector;
    public ConnectConnector m_NegativeConnector;

    //Pantalla del handTerminal.
    [SerializeField] private GameObject mConTexts;

    //Renglones de la pantalla (Barras verdes para este caso).
    [SerializeField] private GameObject mImag2;
    [SerializeField] private GameObject mImag3;
    [SerializeField] private GameObject mImag4;

    //Text de los renglones mencionados anteriormente.
    [SerializeField] private Text mText1;
    [SerializeField] private Text mText2;
    [SerializeField] private Text mText3;
    [SerializeField] private Text mText4;

    [SerializeField] private TransmitterScript mTransmisor;

    //Rango maximo y minimo inicial que tiene el handTerminal.
    public double mRangoMax { get; set;} = 2;
    public double mRangoMin { get; set; } = 0;

    //Condicion que me dice que valor se quiere modificar si LVR (false) o UVR (true)
    private bool conLVRUVR = false;
    //Condicion que me dice si se quiere modificar un valor.
    private bool conModificación = false;
    //Condicion que me dice si se quiere actualizar los valores de procesos.
    private bool actValoresProceso = false;
    //Para habilitar el uso de decimales cuando se esta modificando un valor.
    private bool habDec = true;

    private double porcentaje;
    //Valor actual
    private string numact;
    //Contador para el caso de renglones (barras verdes)
    private int con1 = 1;

    private void Start() {
        mRangoMax = mTransmisor.getActualUVR();;
        mRangoMin = mTransmisor.getActualLVR();
    }

    void Update()
    {
        CheckConnections();
        if (m_InstrumentState)
        {
            if (mCondOnOff == true)
            {
                mConTexts.SetActive(true);
                if (actValoresProceso == true)
                {
                    mText1.text = "VARIABLE PROC.";
                    mText2.text = "Corriente : " + (mTransmisor.m_Corriente * 1000) + " mA";
                    porcentaje = mTransmisor.m_Corriente - 4 * Mathf.Pow(10, -3);
                    porcentaje = (porcentaje * 100) / (16 * Mathf.Pow(10, -3));
                    porcentaje = System.Math.Round(porcentaje, 2);
                    mText3.text = "Porcentaje: " + porcentaje + " %";
                    mText4.text = "Unidad ing: " + mTransmisor.m_Presion + " psi";
                }
            }
            else
            {
                mConTexts.SetActive(false);
            }
        }
    }

    // FUNCIONES PRINCIPALES...
    private void ActualizarDato(string num)
    {
        if (conModificación == true)
        {
            numact = string.Concat(numact, num);
            if (conLVRUVR == false)
            {
                mText4.text = "LVR nuevo : "+ numact;
            }
            else 
            {
                mText4.text = "UVR nuevo : " + numact;
            }
           
        }
    }

    public void EjecutarBtn(string opc)
    {
        if (mCondOnOff == false && opc == "On/Off" && m_InstrumentState) // Para encender el handterminal
        {
            mCondOnOff = true;
        }
        else if (mCondOnOff) // Si esta Encendido permite...
        {
            EjecutarBtnsOn(opc);
        }
    }

    private void EjecutarBtnsOn(string opc) 
    {
        switch (opc)
        {
            case "On/Off":
                mCondOnOff = false;
                break;

                //TECLADO NUMERICO
            case "1":
                ActualizarDato("1");
                break;
            case "2":
                ActualizarDato("2");
                break;
            case "3":
                ActualizarDato("3");
                break;
            case "4":
                ActualizarDato("4");
                break;
            case "5":
                ActualizarDato("5");
                break;
            case "6":
                ActualizarDato("6");
                break;
            case "7":
                ActualizarDato("7");
                break;
            case "8":
                ActualizarDato("8");
                break;
            case "9":
                ActualizarDato("9");
                break;
            case "0":
                ActualizarDato("0");
                break;
            //Algunos mas
            case "-":
                break;
            case ".":
                if (habDec == true) 
                {
                    if(Application.platform == RuntimePlatform.WebGLPlayer)
                        ActualizarDato(".");
                    else
                        ActualizarDato(",");
                    habDec = false;
                }
                break;
            case "^":
                DesplazarUpDownMenu(-1);
                break;
            case "v":
                DesplazarUpDownMenu(1);
                break;
            case "<": //Si deseo volver al menu anteior...

                //DE NIVEL2 A MENU PRINCIPAL
                if (mText1.text == "MODIFICAR LVR. UVR" && mText2.text == "¿Cual deseas modificar?")
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";
                    break;
                }

                if (mText1.text == "LECTURA VALORES LVR UVR")
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";
                    break;
                }

                if (mText1.text == "VARIABLE PROC.")
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";
                    actValoresProceso = false;
                    break;
                }

                // DE MODIFICACION LVR O UVR A MODIFICAR LVR. UVR.
                if (mText1.text == "MODIFICAR LVR" || mText1.text == "MODIFICAR UVR")
                {
                    mText1.text = "MODIFICAR LVR. UVR";
                    mText2.text = "¿Cual deseas modificar?";
                    mText3.text = "LVR: " + mRangoMin;
                    mText4.text = "UVR: " + mRangoMax;

                    numact = string.Empty;
                    conModificación = false;
                    habDec = true;
                    break;
                }

                break;

            case ">": //Tambien Funciona para Enter

                //NIVEL 1 MENU PRINCIPAL
                if (con1 == 1 && mText2.text == "Modificar LVR. UVR.")
                {
                    mText1.text = "MODIFICAR LVR. UVR";
                    mText2.text = "¿Cual deseas modificar?";
                    mText3.text = "LVR: " + mRangoMin;
                    mText4.text = "UVR: " + mRangoMax;
                    break;
                }

                if (con1 == 2 && mText3.text == "Lectura valores actuales LVR UVR.")
                {
                    mText1.text = "LECTURA VALORES LVR UVR";
                    mText2.text = "Valores actuales:";
                    mText3.text = "LVR: " + mRangoMin;
                    mText4.text = "UVR: " + mRangoMax;
                    break;
                }

                if (con1 == 3 && mText4.text == "Lectura variable del proceso.")
                {

                    mText1.text = "VARIABLE PROC.";
                    mText2.text = "Corriente : " + (mTransmisor.m_Corriente*1000) + " mA";
                    porcentaje = mTransmisor.m_Corriente - 4 * Mathf.Pow(10, -3);
                    porcentaje = (porcentaje * 100) / (16 * Mathf.Pow(10, -3));
                    mText3.text = "Porcentaje: " + porcentaje + " %";
                    mText4.text = "Unidad ing: " + mTransmisor.m_Presion + " psi";
                    actValoresProceso = true;
                    break;
                }

                // MODIFICAR LVR.
                if (con1 == 2 && mText3.text == ("LVR: " + mRangoMin) && mText1.text == "MODIFICAR LVR. UVR")
                {
                    mText1.text = "MODIFICAR LVR";
                    mText2.text = "Digite el nuevo valor:";
                    mText3.text = "LVR actual: " + mRangoMin;
                    mText4.text = "LVR nuevo : ";
                    conLVRUVR = false;
                    conModificación = true;
                    habDec = true;
                    break;
                }

                if (mText1.text == "MODIFICAR LVR" && mText2.text == "Digite el nuevo valor:")
                {
                    mText1.text = "MODIFICAR LVR";
                    mText2.text = "¿Aceptar cambios?";
                    mText3.text = "Si";
                    mText4.text = "No";
                    break;
                }

                //SI ACEPTO CAMBIO DE LVR
                if (mText1.text == "MODIFICAR LVR" && mText2.text == "¿Aceptar cambios?" && con1 == 2)
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";
                    ActualizarLVRoUVR();
                    conModificación = false;
                    habDec = true;
                    break;
                }

                //NO ACEPTO CAMBIO DE LVR
                if (mText1.text == "MODIFICAR LVR" && mText2.text == "¿Aceptar cambios?" && con1 == 3)
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";
                    numact = string.Empty;
                    conModificación = false;
                    habDec = true;
                    break;
                }

                //MODIFICANDO UVR
                if (con1 == 3 && mText4.text == ("UVR: " + mRangoMax) && mText1.text == "MODIFICAR LVR. UVR")
                {
                    mText1.text = "MODIFICAR UVR";
                    mText2.text = "Digite el nuevo valor:";
                    mText3.text = "UVR actual: " + mRangoMax;
                    mText4.text = "UVR nuevo : ";
                    conLVRUVR = true;
                    conModificación = true;
                    habDec = true;
                    break;
                }

                if (mText1.text == "MODIFICAR UVR" && mText2.text == "Digite el nuevo valor:")
                {
                    mText1.text = "MODIFICAR UVR";
                    mText2.text = "¿Aceptar cambios?";
                    mText3.text = "Si";
                    mText4.text = "No";
                    break;
                }

                //SI ACEPTO EL CAMBIO DE UVR
                if (mText1.text == "MODIFICAR UVR" && mText2.text == "¿Aceptar cambios?" && con1 == 2)
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";
                    ActualizarLVRoUVR();
                    conModificación = false;
                    habDec = true;
                    break;
                }

                //NO ACEPTO EL CAMBIO DE UVR
                if (mText1.text == "MODIFICAR UVR" && mText2.text == "¿Aceptar cambios?" && con1 == 3)
                {
                    mText1.text = "MENU INICIO";
                    mText2.text = "Modificar LVR. UVR.";
                    mText3.text = "Lectura valores actuales LVR UVR.";
                    mText4.text = "Lectura variable del proceso.";

                    numact = string.Empty;
                    conModificación = false;
                    habDec = true;
                    break;
                }

                break;
        }
    }

    private void DesplazarUpDownMenu(int mcon)
    {
        con1 += mcon; 

        //Condiciones de protección.
        if (con1 <= 0) con1 = 1;
        if (con1 >= 4) con1 = 3;

        //Ajuste de los renglones (barras verdes).
        switch (con1) 
        {
            case 1:
                mImag2.SetActive(true);
                mImag3.SetActive(false);
                mImag4.SetActive(false);
                break;
            case 2:
                mImag2.SetActive(false);
                mImag3.SetActive(true);
                mImag4.SetActive(false);
                break;
            case 3:
                mImag2.SetActive(false);
                mImag3.SetActive(false);
                mImag4.SetActive(true);
                break;
        }
    
    }

    private void ActualizarLVRoUVR() 
    {
        if (conLVRUVR == false) // Para LVR
        {
            //En caso que el valor enviado para modificar LVR o UVR sea vacio.
            if (numact == string.Empty) 
            {
                mRangoMin = 0;
            }
            else
            {
                mRangoMin = double.Parse(numact);
                mRangoMin = Math.Round(mRangoMin, 2);
            }
            numact = string.Empty;
            conLVRUVR= true;
        }
        else // PARA UVR
        {
            if (numact == string.Empty) 
            {
                mRangoMax = 0;
            }
            else
            {
                mRangoMax = double.Parse(numact);
                mRangoMax = Math.Round(mRangoMax, 2);
            }
            numact = string.Empty;
            conLVRUVR = false;
        }
        FindObjectOfType<MPGameMgr>().photonView.RPC("SyncLVRUVR",RpcTarget.Others,mRangoMin,mRangoMax);
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
