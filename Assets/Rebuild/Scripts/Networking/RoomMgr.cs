using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class RoomMgr : MonoBehaviourPunCallbacks
{
    public Transform panelRooms, panelPlayers;
    public GameObject prefabRoom, prefabPlayer, botonStart, loadingMsg;
    public UIRoom selectedRoom;
    public TextMeshProUGUI roomName, parameters;
    public TMP_InputField sesionName;
    MainSceneController mgr;

    private void Start() {
        mgr = FindObjectOfType<MainSceneController>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(!PhotonNetwork.InRoom)
        {
            foreach (UIRoom room in panelRooms.GetComponentsInChildren<UIRoom>())
                Destroy(room.gameObject);
            foreach (RoomInfo room in roomList)
                Instantiate(prefabRoom, panelRooms).GetComponent<UIRoom>().InitData(room.Name, room.PlayerCount);
        }
        Debug.Log("Rooms: " + roomList.Count);
    }
    
    public void CrearRoom()
    {
        mgr.SetStatusText("Creando sala");
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.EmptyRoomTtl = 0;
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, options);
    }

    public void SelectedRoom(UIRoom room)
    {
        selectedRoom?.Deselected();
        selectedRoom = room;
    }

    public void GoBackToLobby()
    {
        mgr.GotoPanel(3);
    }

    public void OnJoinClicked()
    {
        mgr.SetStatusText("Uniendose a una sala...");
        if (selectedRoom)
            PhotonNetwork.JoinRoom(selectedRoom.roomName);
        else
            PhotonNetwork.JoinRandomRoom();
    }

    public void OnJoinByNameClicked()
    {
        mgr.SetStatusText("Uniendose a una sala...");
        PhotonNetwork.JoinRoom(sesionName.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        mgr.MsgBox("Fallo al unirse a una sala " + message);
        GoBackToLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        mgr.MsgBox("Fallo al unirse a una sala " + message);
        GoBackToLobby();
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
        GoBackToLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        mgr.GotoPanel(5);
        roomName.text = "Sesión de " + PhotonNetwork.CurrentRoom.Name;
        PlayerListing();
        if (PhotonNetwork.IsMasterClient)
        {
            botonStart.SetActive(true);  
            //parameters.text = $"Altura: {Ctrller.alturaTanque}Cm \nAltura Toma: {Ctrller.alturaToma}Cm \nDensidad relativa: {Ctrller.densidadRelativa}";
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerListing();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListing();
        if(PhotonNetwork.IsMasterClient)
        {
            FindObjectOfType<Practice1Parameters>().SendParameters(newPlayer);
        }
    }

    void PlayerListing()
    {
        Player[] currPlayers = PhotonNetwork.PlayerList;
        foreach(TMPro.TextMeshProUGUI c in panelPlayers.GetComponentsInChildren<TextMeshProUGUI>())
            Destroy(c.gameObject);

        foreach (Player p in currPlayers)
            Instantiate(prefabPlayer, panelPlayers).GetComponent<TextMeshProUGUI>().text = p.NickName;
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("PracticaPresion");
    }

    public void BtnSalir()
    {
        PhotonNetwork.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    private void Update() 
    {
        if(PhotonNetwork.LevelLoadingProgress>0 && PhotonNetwork.LevelLoadingProgress<0.9f)
        {
            loadingMsg.SetActive(true);
            mgr.GotoPanel(7);
            loadingMsg.GetComponent<TextMeshProUGUI>().text = "Cargando...  " + PhotonNetwork.LevelLoadingProgress*100 + "%";
        }
    }
}
