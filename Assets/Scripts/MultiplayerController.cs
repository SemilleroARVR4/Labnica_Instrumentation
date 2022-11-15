using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start() {
        CursorManager.LoadCursors();
        PhotonNetwork.IsMessageQueueRunning = true;
        CreatePlayer();
    }

    void CreatePlayer()
    {
        int offset = 0;
        List<Player> playerList = new List<Player>();
        playerList.AddRange(PhotonNetwork.PlayerList);
        offset = playerList.IndexOf(PhotonNetwork.LocalPlayer);
        PhotonNetwork.Instantiate("Player",  Vector3.forward*2 + Vector3.right*offset*0.5f, Quaternion.identity);
    }
}
