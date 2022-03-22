using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MPGameMgr : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start() {
        CursorManager.LoadCursors();
        PhotonNetwork.IsMessageQueueRunning = true;
        if(PhotonNetwork.OfflineMode)
        {
            Debug.Log("Llamado a offline player");
            CrearOfflinePlayer();
            return;
        }
        CrearPlayer();
    }

    void CrearPlayer()
    {
        int offset = 0;
        List<Player> playerList = new List<Player>();
        playerList.AddRange(PhotonNetwork.PlayerList);
        offset = playerList.IndexOf(PhotonNetwork.LocalPlayer);
        PhotonNetwork.Instantiate("MultiplayerStudent", Vector3.right*offset*0.5f, Quaternion.identity);
    }

    void CrearOfflinePlayer()
    {
        Debug.Log("Player offline creado");
        GameObject player = Resources.Load<GameObject>("MultiplayerStudent");
        Instantiate(player,Vector3.zero,Quaternion.identity);
    }


    [PunRPC]
    void CrearCable()
    {
        CreateCable cableMgr = FindObjectOfType<CreateCable>();
        Photon.Pun.PhotonNetwork.InstantiateRoomObject("Cable",cableMgr._cableParent.transform.position,cableMgr.cableAngle);
    }

    [PunRPC]
    void PowerOn(){
        FindObjectOfType<PowerSupplyScript>().TurnOnCircuit(true);
    }

    [PunRPC]
    void PowerOff(){
        FindObjectOfType<CircuitManager>().ResetCircuit(true);
    }


    [PunRPC]
    void SyncLVRUVR(double LVR, double UVR)
    {
        HandTerminal ht = FindObjectOfType<HandTerminal>();
        ht.mRangoMax = UVR;
        ht.mRangoMin = LVR;
    }

    public void QuitGame()
    {
        if(PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
            StartCoroutine(SendSesionStatus(newPlayer));
    }

    IEnumerator SendSesionStatus(Player newPlayer)
    {
        photonView.RPC("RpcSyncSesionParameters",newPlayer, Ctrller.alturaTanque,Ctrller.alturaToma,Ctrller.densidadRelativa);
        PhotonNetwork.SendAllOutgoingCommands();
        HandTerminal ht = FindObjectOfType<HandTerminal>();
        photonView.RPC(nameof(SyncLVRUVR),newPlayer,ht.mRangoMin,ht.mRangoMax);
        FindObjectOfType<SyncTank>().SyncAll();
        yield return new WaitForSeconds(0.3f);
        foreach(NetCable c in FindObjectsOfType<NetCable>())
        {
            if(c.alreadyAccepted)
                c.CallRpc("AcceptCableConnector",0,newPlayer);
        }
        yield return new WaitForSeconds(0.3f);
        if(FindObjectOfType<PowerSupplyScript>().m_InstrumentState)
            photonView.RPC(nameof(PowerOn),newPlayer);
    }

    [PunRPC]
    void RpcSyncSesionParameters(int altura, float alturaToma, float densidadRelativa)
    {
        Ctrller.alturaTanque = altura;
        Ctrller.alturaToma = alturaToma;
        Ctrller.densidadRelativa = densidadRelativa;
        FindObjectOfType<TankResizable>().BroadcastMessage("Start");
        FindObjectOfType<EscalableTransmitter>().BroadcastMessage("Start");
    }
}


