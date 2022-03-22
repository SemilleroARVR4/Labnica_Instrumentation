using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Ctrller : MonoBehaviour
{
    AsyncOperation asyncLoad;
    public static int alturaTanque = 150;
    public static float alturaToma = 10, densidadRelativa = 1;
    public TMP_InputField inputAlturaTanque, inputAlturaToma, inputDensidad;

    // Start is called before the first frame update
    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void SetOfflineMode()
    {
        if(Photon.Pun.PhotonNetwork.IsConnected)
            Photon.Pun.PhotonNetwork.Disconnect();
            
        Photon.Pun.PhotonNetwork.OfflineMode = true;
    }

    public void OnAcepptedParams()
    {
        alturaTanque = int.Parse(inputAlturaTanque.text);
        alturaToma = float.Parse(inputAlturaToma.text);
        densidadRelativa = float.Parse(inputDensidad.text);
        if(PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.CreateRoom("Offline");
            PhotonNetwork.LoadLevel("EscenaMultiplayer");
        }
        else
            FindObjectOfType<RoomMgr>().CrearRoom();
    }

}
