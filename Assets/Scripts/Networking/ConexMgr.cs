using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class ConexMgr : MonoBehaviourPunCallbacks
{
    public TMP_InputField txtNick;
    public TextMeshProUGUI estado;
    public GameObject panelConex, panelLobby;


    public void Connect2Master()
    {
        Debug.Log("Llamado a Connect2Master");
        string nickName = txtNick.text;
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        estado.gameObject.SetActive(true);
    }

    private void OnGUI()
    {
        if (estado && estado.gameObject.activeSelf)
            estado.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado, causa: " + cause.ToString());
    }

    public override void OnConnectedToMaster()
    {
        if(!PhotonNetwork.OfflineMode)
            Invoke(nameof(LoadRoomPanel),0.5f);
    }

    void LoadRoomPanel()
    {
        panelLobby.SetActive(true);
        estado.gameObject.SetActive(false);
        PhotonNetwork.JoinLobby();
    }
}
