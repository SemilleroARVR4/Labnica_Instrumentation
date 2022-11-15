using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    static MessageBox singleton;
    public TMPro.TextMeshProUGUI titleTxt, msgTxt;

    private void Awake() {
        singleton = this;
    }

    public static void ShowMessage(string title, string msg)
    {
        singleton.titleTxt.text = title;
        singleton.msgTxt.text = msg;
        singleton.titleTxt.transform.parent.gameObject.SetActive(true);
    }

    public static void AcceptPressed()
    {
        singleton.gameObject.SetActive(false);
    }
}
