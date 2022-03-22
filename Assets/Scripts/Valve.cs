using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    public bool status, isInValve;   //True = Abierta, False = Cerrada
    public GameObject efectoAgua;

    private void OnMouseDown() {
        status = !status;
        SetStatus();
        FindObjectOfType<SyncTank>().OnValveStatusChange(isInValve,status);
    }

    public void SetStatus()
    {
        GetComponentInChildren<TMPro.TextMeshPro>().text = status? "Estado:\nAbierta" : "Estado:\nCerrada";
        efectoAgua.SetActive(status);
    }
}
