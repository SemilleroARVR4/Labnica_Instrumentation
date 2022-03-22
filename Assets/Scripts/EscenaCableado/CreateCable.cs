using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateCable : MonoBehaviourPun
{
    [SerializeField] CircuitManager circuitManager;
    [SerializeField] GameObject cableInstance;
    [SerializeField] GameObject cableParent;
    public Quaternion cableAngle;

    public GameObject _cableParent{get{return cableParent;}}
    
    public float cableDistance;

    public int _cableIDSequence = 0;

    // Update is called once per frame
    void Update()
    {
        if (!circuitManager._powerSupply.GetComponent<PowerSupplyScript>().m_InstrumentState && CheckCanCreate())
        {
            //Al presionar C se crea un nuevo cable.
            if (Input.GetKeyDown(KeyCode.C))
            {
                FindObjectOfType<MPGameMgr>().photonView.RPC("CrearCable",RpcTarget.MasterClient);
                /*
                GameObject cableClone = Instantiate(cableInstance, cableParent.transform.position, cableAngle, cableParent.transform);
                cableClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                //Para BD ... Aplicar _cableID a cada cable poner fecha y hora de creacion
                UpdateCableInfo(cableClone);
                _cableIDSequence++;
                */
            }
        }
        /*
        else if (circuitManager._powerSupply.GetComponent<PowerSupplyScript>().m_InstrumentState)
        {
            GameObject[] cableList = GameObject.FindGameObjectsWithTag("Cable");
            foreach (GameObject cable in cableList)
            {
                if (!cable.transform.GetChild(0).GetComponent<ConnectCable>().touchedObject || !cable.transform.GetChild(1).GetComponent<ConnectCable>().touchedObject)
                {
                    Destroy(cable);
                }
            }
        }
        */
    }
    public bool CheckCanCreate()
    {
        bool _onlyOneCable = true;

        GameObject[] cableList = GameObject.FindGameObjectsWithTag("Cable");
        foreach(GameObject cable in cableList)
        {
            if (!cable.transform.GetChild(0).GetComponent<ConnectCable>().DisconnectCanvas.activeSelf || !cable.transform.GetChild(1).GetComponent<ConnectCable>().DisconnectCanvas.activeSelf)
            {
                _onlyOneCable = false;
                return _onlyOneCable;
            }
            else
            {
                _onlyOneCable = true;
            }
        }
        return _onlyOneCable;
    }

    public void UpdateCableInfo(GameObject _cableInfo)
    {
        //Extraemos la informacion de la session y del chequeo en curso.
        TxtController _DBRegister = GameObject.FindGameObjectWithTag("DBRegister").GetComponent<TxtController>();

        //Actualizamos la informacion del cable al crearse.
        _cableInfo.GetComponent<CableInfo>()._sessionID = _DBRegister._sessionRegister._sessionID;
        _cableInfo.GetComponent<CableInfo>()._checkID = _DBRegister._checkRegister._checkID;
        _cableInfo.GetComponent<CableInfo>()._cableID = _cableIDSequence;
        _cableInfo.GetComponent<CableInfo>()._startTimeCable = System.DateTime.Now;
    }
}
