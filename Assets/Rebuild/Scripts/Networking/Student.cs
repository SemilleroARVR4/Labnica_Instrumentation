using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Student : MonoBehaviourPun
{
    public Transform cameraPos;
    public TMPro.TextMeshPro textName;

    Vector3 normalPos = new Vector3(0,1.76F,0.2F);
    Vector3 crouchPos = new Vector3(0,1,0.2F);

    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.OfflineMode && photonView.Owner != null)
            textName.text = photonView.Owner.NickName;

        if(!PhotonNetwork.OfflineMode && !photonView.IsMine)
            return;

        Camera.main.transform.SetParent(cameraPos);
        Camera.main.transform.localPosition = Vector3.zero;

        GetComponent<FirstPersonMovement>().enabled = true;
        Camera.main.GetComponent<FirstPersonLook>().SendMessage("Reset");
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.AmOwner)
            textName.transform.LookAt(Camera.main.transform);
        if(Input.GetKeyDown(KeyCode.LeftShift))
            cameraPos.localPosition = crouchPos;
        if(Input.GetKeyUp(KeyCode.LeftShift))
            cameraPos.localPosition = normalPos;
    }
}
