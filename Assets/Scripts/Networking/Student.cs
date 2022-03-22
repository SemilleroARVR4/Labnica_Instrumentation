using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Student : MonoBehaviourPun
{
    public Transform cameraPos;
    public TMPro.TextMeshPro textName;

    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.OfflineMode)
            textName.text = photonView.Owner.NickName;

        if(!PhotonNetwork.OfflineMode && !photonView.IsMine)
            return;

        Camera.main.transform.position = cameraPos.position;
        Camera.main.transform.parent = transform;
        Camera.main.SendMessage("Reset");
        GetComponent<FirstPersonMovement>().enabled = true;
        GetComponent<Jump>().enabled = true;
        GetComponent<Crouch>().enabled = true;
        GetComponent<Crouch>().headToLower = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        textName.transform.LookAt(Camera.main.transform);
    }
}
