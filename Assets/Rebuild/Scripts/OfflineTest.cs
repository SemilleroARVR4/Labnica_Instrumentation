using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        Photon.Pun.PhotonNetwork.OfflineMode = true;
        Photon.Pun.PhotonNetwork.CreateRoom("Offline");
    }
}
