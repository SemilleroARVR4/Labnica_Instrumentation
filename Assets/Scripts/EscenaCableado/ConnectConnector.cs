using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConnectConnector : MonoBehaviour
{
    
    public string iniTag;
    public bool m_AlreadyCheck = false;
    public GameObject m_pairConnector;
    public CircuitManager m_CircuitManager;

    //public string[] endTag;
    public GameObject[] endGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        //endTag = new string[10];
        endGameObjects = new GameObject[10];
/*
        for (int i = 0; i < 10; i++)
        {
            endTag[i] = "";
        }
        */
        m_CircuitManager = GameObject.FindGameObjectWithTag("CircuitManager").GetComponent<CircuitManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CheckArrayNumber()
    {
        int j = 0;

        for (int k = 0; k < endGameObjects.Length; k++)
        {
            if (endGameObjects[k] == null || !endGameObjects[k])
            {
                j = k;
                return j;
            }
        }
        return j;
    }

    public void ConnectCircuit(GameObject m_Connectedfrom)
    {
        if (m_AlreadyCheck == false)
        {
            m_AlreadyCheck = true;

            if (CheckArrayNumber() > 1 && m_Connectedfrom.GetComponent<ConnectConnector>().iniTag == "negative" && m_pairConnector.GetComponent<ConnectConnector>().m_AlreadyCheck == true)
            {
                //Logica paralelo
                //Debug.Log("El connector " + gameObject.name + " del dispositivo " + this.gameObject.transform.parent.transform.parent.name + " cerró paralelo con " + CheckArrayNumber() + " conexiones.");
                //-------------------CODIGO PARA VERIFICAR EL RESTO DE DISPOSITIVOS CONECTADOS A ESTE PUNTO--------------------//
                //Todos los dispoitivos que esten conectados a este punto, deben verificarse con este mismo punto
                foreach (GameObject conexion in endGameObjects)
                {
                    if (conexion != null)
                    {
                        conexion.GetComponent<ConnectConnector>().ConnectCircuit(this.gameObject);
                    }
                }
            }
            else
            {
                //logica serie
                //Debug.Log("Chequeando conector " + gameObject.name + " de " + transform.parent.transform.parent.name + " es llamado por el conector " + m_Connectedfrom.name + " del dispositivo " + m_Connectedfrom.transform.parent.transform.parent);

                //-------------------CODIGO PARA VERIFICAR CONEXIONES CON LA FUENTE DE ALIMENTACION--------------------//
                //Si el conector que me invoca proviene de la fuente de alimentacion, entonces debo mantenerme positivo 
                if (m_Connectedfrom.transform.parent.transform.parent.tag == "PowerSupply")
                {
                    iniTag = "positive";
                    m_pairConnector.GetComponent<ConnectConnector>().iniTag = "negative";
                }

                // Si el objeto actual es el negativo de la fuente y es llamado por un conector que es negativo, entonces significa que se cerro el lazo
                if (gameObject.transform.parent.transform.parent.TryGetComponent<PowerSupplyScript>(out PowerSupplyScript m_PowerSupply) && m_Connectedfrom.GetComponent<ConnectConnector>().iniTag == "negative")
                {
                    m_CircuitManager.m_ClosedCircuit = true;

                    Debug.Log("El lazo se cierra con estado: " + m_CircuitManager.m_ClosedCircuit.ToString());
                    return;
                }



                //-------------------CODIGO PARA VERIFICAR CONEXIONES EL DISPOSITIVO QUE CONVOCA A ESTE PUNTO--------------------//
                // Si el conector que me invoca es positivo, debo revisar si es mi pareja o si es otro dispositivo 
                if (m_Connectedfrom.GetComponent<ConnectConnector>().iniTag == "positive")
                {
                    //Si el conector que me llama es mi pareja, entonces cambio de signo
                    if (m_Connectedfrom == m_pairConnector)
                    {
                        if (endGameObjects[0] != null)
                        {
                            iniTag = "negative";
                        }
                        //Debug.Log("El connector " + gameObject.name + " del dispositivo " + this.gameObject.transform.parent.transform.parent.name + " TIENE UN LLAMADO DE SU PAREJA Y QUEDA " + iniTag);
                    }
                    else
                    {
                        if (endGameObjects[0] != null)
                        {
                            iniTag = "positive";
                        }
                        m_pairConnector.GetComponent<ConnectConnector>().iniTag = "negative";
                        //Debug.Log("El connector " + gameObject.name + " del dispositivo " + this.gameObject.transform.parent.transform.parent.name + " TIENE UN LLAMADO POSITIVO Y QUEDA " + iniTag);
                    }
                }
                // Si el conector que me invoca, el objeto es negativo, debo pasar a ser positivo
                else if (m_Connectedfrom.GetComponent<ConnectConnector>().iniTag == "negative")
                {
                    iniTag = "positive";
                    m_pairConnector.GetComponent<ConnectConnector>().iniTag = "negative";
                    //Debug.Log("El connector " + gameObject.name + " del dispositivo " + this.gameObject.transform.parent.transform.parent.name + " TIENE UN LLAMADO NEGATIVO Y QUEDA " + iniTag);
                }

                //-------------------CODIGO PARA VERIFICAR EL RESTO DE DISPOSITIVOS CONECTADOS A ESTE PUNTO--------------------//
                //Todos los dispoitivos que esten conectados a este punto, deben verificarse con este mismo punto
                foreach (GameObject conexion in endGameObjects)
                {
                    if (conexion != null)
                    {
                        conexion.GetComponent<ConnectConnector>().ConnectCircuit(this.gameObject);
                    }
                }

                //Hacer la verificacion de mi pareja, antes de poner al resto de conexiones en el conector
                m_pairConnector.GetComponent<ConnectConnector>().ConnectCircuit(gameObject);

                //Debug.Log("El connector " + gameObject.name + " del dispositivo " + this.gameObject.transform.parent.transform.parent.name + " queda con la etiqueta " + iniTag);
            }

        }
    }
}