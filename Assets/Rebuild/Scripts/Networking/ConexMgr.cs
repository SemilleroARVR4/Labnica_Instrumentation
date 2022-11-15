using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class ConexMgr : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI estado;

    public void Connect2Master()
    {
        Debug.Log("Llamado a Connect2Master");
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = MainSceneController.playerName;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        FindObjectOfType<MainSceneController>().GotoPanel(6);
    }

    private void OnGUI()
    {
        if (!PhotonNetwork.IsConnectedAndReady && estado && estado.gameObject.activeSelf)
            estado.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        FindObjectOfType<MainSceneController>().MsgBox("Desconectado: " + cause.ToString());
    }

    public override void OnConnectedToMaster()
    {
        print("Connected To Master");
        if(!PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.JoinLobby();
            FindObjectOfType<MainSceneController>().GotoPanel(3);
        }
    }
}
