using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetCable : MonoBehaviourPun
{
    public ConnectCable cable1, cable2;

    public bool alreadyAccepted;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(FindObjectOfType<CreateCable>()._cableParent.transform);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }

    public void CallRpc(string name, int first)
    {
        photonView.RPC(nameof(RpcCalled),RpcTarget.Others,name, first ==0? true: false);
    }

    public void CallRpc(string name, int first, Player p)
    {
        photonView.RPC(nameof(RpcCalled),p,name, first ==0? true: false);
    }

    [PunRPC]
    void RpcCalled(string name, bool first)
    {
        if(first)
            cable1.Invoke(name,0);
        else
            cable2.Invoke(name,0);
    }

}
