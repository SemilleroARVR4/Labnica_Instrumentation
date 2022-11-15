using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainSceneController : MonoBehaviour
{
    [Tooltip("0 = Start Panel \n1= ModePanel \n2= OptionsPanel \n3= LobbyPanel \n4= PracticeParams \n5= RoomPanel")]
    public GameObject[] panels;
    public int status;
    public TMPro.TMP_InputField txtName;
    public static string playerName;
    public TMPro.TextMeshProUGUI msgBox;
    public GameObject backGround;

    private void Awake() {
        backGround.SetActive(true);
        Application.targetFrameRate = 60;
    }

    public void GotoPanel(int index)
    {
        if(index == 1)
            if(txtName.text == "")
            {
                MsgBox("Ingrese un nombre");
                return;
            }
            else
                playerName = txtName.text;

        status = index;
        foreach (var item in panels)
            item.SetActive(false);
        panels[status].SetActive(true);

        if(status == 4)
            backGround.SetActive(false);
        else
            backGround.SetActive(true);
    }

    public void SetStatusText(string text)
    {
        GotoPanel(6);
        panels[6].GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void MsgBox(string txt)
    {
       msgBox.transform.parent.gameObject.SetActive(true);
       msgBox.text = txt;
    }

    public void SetOfflineMode()
    {
        if(Photon.Pun.PhotonNetwork.IsConnected)
            Photon.Pun.PhotonNetwork.Disconnect();
        Photon.Pun.PhotonNetwork.OfflineMode = true;
        GotoPanel(4);
    }

    public void OnAcepptedParams()
    {
        if(PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.CreateRoom("Offline");
            PhotonNetwork.LoadLevel("PracticaPresion");
        }
        else
            FindObjectOfType<RoomMgr>().CrearRoom();

        FindObjectOfType<Practice1Parameters>().SetParameters();
    }
}
