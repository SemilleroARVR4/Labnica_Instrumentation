using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;




public class CableManager : MonoBehaviourPun
{
    public Transform cableParent;
    public int cableIndex;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(GameObject.Find("NewCable") == null)
            {
                PhotonNetwork.Instantiate("NewCable", cableParent.position, cableParent.rotation);
                photonView.RPC(nameof(RPCCreateCable),RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    void RPCCreateCable()
    {
        cableIndex++;
        photonView.RPC(nameof(RPCUpdateIndex),RpcTarget.MasterClient,cableIndex);
    }

    [PunRPC]
    void RPCUpdateIndex(int newIndex)
    {
        cableIndex = newIndex;
    }
}
