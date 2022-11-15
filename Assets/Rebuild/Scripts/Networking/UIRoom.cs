using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIRoom : MonoBehaviour
{
    public string roomName;
    public void InitData(string name, int numPlayers)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "Sesión de " +  name + "   " + numPlayers + "/4";
        roomName = name;
    }

    public void OnClicked()
    {
        FindObjectOfType<RoomMgr>().SelectedRoom(this);
        GetComponent<Image>().color = new Color(0.7f, 0.7f, 1);
    }

    public void Deselected()
    {
        GetComponent<Image>().color = Color.white;
    }
}
