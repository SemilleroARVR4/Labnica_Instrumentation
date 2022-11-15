using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncTank : MonoBehaviourPun
{
    TankEscalable tank;

    private void Awake() {
        tank = FindObjectOfType<TankEscalable>();
    }

    public void OnValveStatusChange(bool isInValve, bool status)
    {
        photonView.RPC("RpcOnValveStatusChange",Photon.Pun.RpcTarget.Others, isInValve, status,tank.actualLevel);
    }

    [PunRPC]
    public void RpcOnValveStatusChange(bool In, bool status, float level)
    {
        if(In)
        {
            tank.valvulaIn.status = status;
            tank.valvulaIn.SetStatus();
        }
        else
        {
            tank.valvulaOut.status = status;
            tank.valvulaOut.SetStatus();
        }
        tank.actualLevel = level;
    }

    public void SyncAll()
    {
        photonView.RPC("RpcOnValveStatusChange",Photon.Pun.RpcTarget.Others, true, tank.valvulaIn.status,tank.actualLevel);
        photonView.RPC("RpcOnValveStatusChange",Photon.Pun.RpcTarget.Others, true, tank.valvulaOut.status,tank.actualLevel);
    }
}
