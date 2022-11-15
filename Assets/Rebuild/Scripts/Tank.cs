using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    //Se refiere al text que tiene el boton que sirve como valvulaIn o valvulaOut.
    [SerializeField] private Text m_textValvula1;  
    [SerializeField] private Text m_textValvula2;
    //Para este caso un cilindro 3D.
    [SerializeField] private GameObject m_Liquido;
    //Text donde aparece el % de contenido del tanque.
    [SerializeField] private Text mTextContenidoTank;

    private float alturaMax = 130; // nivel máximo del tanque
    public float nivel = 0; //capacidad en litros
    public float offsetTomaAlta = 10;
    private bool m_ValvulaInOpen = false;
    private bool m_ValvulaOutOpen = false;
    public float m_Presion = 0;

    private void Update()
    {
        nivel += ((m_ValvulaInOpen? 50:0)-(m_ValvulaOutOpen? 50:0))*Time.deltaTime*0.5f;

        if(nivel<0)
            nivel = 0;
        if(nivel> alturaMax)
            nivel = alturaMax;

        float escala = nivel/100;
        
        escala = escala == 0? 0.001f: escala;
        m_Liquido.transform.localScale = new Vector3(1, escala,1);

        mTextContenidoTank.text = (100*nivel/alturaMax).ToString("F0")+"%";
        
        m_Presion = (nivel-offsetTomaAlta)/70.307f;
        if(m_Presion<0)
            m_Presion = 0;
    }

    public void EjecutarBtn(string opcion)
    {
        EjecutarBtn(opcion, false);
    }
    
    public void EjecutarBtn(string opcion, bool remote)
    {
        // Esta funcion esta enfocada a usar botones2D como valvulas para el contenido del tanque.
        switch (opcion)
        {
            case "ValvulaIn":
                m_ValvulaInOpen = !m_ValvulaInOpen;
                m_textValvula1.text = m_ValvulaInOpen? "ON" : "OFF";
                if(!remote)
                    FindObjectOfType<SyncTank>().photonView.RPC("onBtnPressed",Photon.Pun.RpcTarget.Others,true);
                break;

            case "ValvulaOut":
                m_ValvulaOutOpen = !m_ValvulaOutOpen;
                m_textValvula2.text = m_ValvulaOutOpen? "ON" : "OFF";
                if(!remote)
                    FindObjectOfType<SyncTank>().photonView.RPC("onBtnPressed",Photon.Pun.RpcTarget.Others,false);
                break;
            default:
                break;
        }

        if(!remote)
            FindObjectOfType<SyncTank>().photonView.RPC("SynchronizeLevel",Photon.Pun.RpcTarget.Others,nivel);
    }

    public void SetNivel(float level)
    {
       nivel = level;
    }
}
