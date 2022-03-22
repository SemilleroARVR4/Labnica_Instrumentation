using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank_BG : MonoBehaviour
{
    public float m_Presion { get; set;} = 0;

    //Se refiere al text que tiene el boton que sirve como valvulaIn o valvulaOut.
    [SerializeField] private Text m_textValvula1;  
    [SerializeField] private Text m_textValvula2;

    //Para este caso un cilindro 3D.
    [SerializeField] private GameObject m_Liquido;

    //Text donde aparece el % de contenido del tanque.
    [SerializeField] private Text mTextContenidoTank;

    public float speed = 0.1f;

    //[SerializeField] private GameObject m_GotaAgua;

    //Cantidad a la que se reduce el liquido (En este caso aunque el n�mero es peque�o es necesario debido a la escala de los obj de escena)
    public float m_Redliquido = 0.035f;
    private float m_delay = 0.7f;
    private float m_TamanoTanque = 0.75f; 
    private bool m_EstadoValvulaIn = false;
    private bool m_EstadoValvulaOut = false;
    private float m_tiempoActual = 0f;
    private float m_tiempoAnt = 0f;

    //Los parametros m_EscalaLiq y m_PosicionLiq son los que ayudan a generar la animaci�n de un tanque con liquido.
    public Vector3 m_EscalaLiq;
    private Vector3 m_PosicionLiq;
    private Animator m_animGota3D;

    private void Awake()
    {
        m_animGota3D = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        m_EscalaLiq = m_Liquido.transform.localScale;
        m_PosicionLiq = m_Liquido.transform.localPosition;

        // Despliega en el text del tanque en % cuanto contenido tiene.
        double porcentaje = (m_EscalaLiq.y * 100) / 0.70f;
        porcentaje = System.Math.Round(porcentaje, 2);
        mTextContenidoTank.text = porcentaje + " %";

        m_tiempoActual = Time.time;
        if ((m_tiempoActual - m_tiempoAnt) >= m_delay)
        {
            m_tiempoAnt = m_tiempoActual;
            if (m_EstadoValvulaIn == true ) AumentarContenido();
            if (m_EstadoValvulaOut == true) DisminuirContenido();

            //Al superar el maximo que se puede contener de liquido en el tanque, este se desborda por una tuberia generando la animacion gota de agua.
            if (m_EscalaLiq.y >= 0.75) 
            {
                //m_animGota3D.enabled = true;
                //m_animGota3D.Play("Gota_de_Agua");
            }
            else
            {
              //m_animGota3D.enabled = false;
              // m_GotaAgua.SetActive(false);
            }

            //Presi�n en el tanque.
            m_Presion = m_EscalaLiq.y;
            m_Presion = (m_Presion * 3f) / 0.70f;
        }
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
                m_EstadoValvulaIn = !m_EstadoValvulaIn;
                if (m_EstadoValvulaIn)
                {
                    m_textValvula1.text = "ON";
                }
                else
                {
                    m_textValvula1.text = "OFF";
                }

                if(!remote)
                    FindObjectOfType<SyncTank>().photonView.RPC("onBtnPressed",Photon.Pun.RpcTarget.Others,true);
                break;
            case "ValvulaOut":
                m_EstadoValvulaOut = !m_EstadoValvulaOut;
                if (m_EstadoValvulaOut)
                {
                    m_textValvula2.text = "ON";
                }
                else
                {
                    m_textValvula2.text = "OFF";
                }

                if(!remote)
                    FindObjectOfType<SyncTank>().photonView.RPC("onBtnPressed",Photon.Pun.RpcTarget.Others,false);
                break;
            default:
                break;
        }
        if(!remote)
            FindObjectOfType<SyncTank>().photonView.RPC("SynchronizeLevel",Photon.Pun.RpcTarget.Others,m_EscalaLiq.y);
    }

    public void SetNivel(float level)
    {
        m_EscalaLiq = new Vector3(m_EscalaLiq.x, level, m_EscalaLiq.x);
    }

    private void AumentarContenido()
    {
        if (m_EscalaLiq.y <= m_TamanoTanque)
        {
            m_Liquido.transform.localScale = new Vector3(m_EscalaLiq.x, m_EscalaLiq.y + m_Redliquido, m_EscalaLiq.z);
            m_Liquido.transform.localPosition = new Vector3(m_PosicionLiq.x, m_PosicionLiq.y + m_Redliquido, m_PosicionLiq.z);

            //m_Liquido.transform.localScale = Vector3.Lerp(m_Liquido.transform.localScale, new Vector3(m_EscalaLiq.x, m_EscalaLiq.y + m_Redliquido, m_EscalaLiq.z), speed * Time.deltaTime);
            //m_Liquido.transform.localPosition = Vector3.Lerp(m_Liquido.transform.localPosition, new Vector3(m_PosicionLiq.x, m_PosicionLiq.y + m_Redliquido, m_PosicionLiq.z), speed * Time.deltaTime);
        }
    }
    private void DisminuirContenido()
    {
        if (m_EscalaLiq.y >= m_Redliquido)
        {      
            m_Liquido.transform.localScale = new Vector3(m_EscalaLiq.x, m_EscalaLiq.y - m_Redliquido, m_EscalaLiq.z);
            m_Liquido.transform.localPosition = new Vector3(m_PosicionLiq.x, m_PosicionLiq.y - m_Redliquido, m_PosicionLiq.z);

            //m_Liquido.transform.localScale = Vector3.Lerp(m_Liquido.transform.localScale, new Vector3(m_EscalaLiq.x, m_EscalaLiq.y - m_Redliquido, m_EscalaLiq.z), speed * Time.deltaTime);
            //m_Liquido.transform.localPosition = Vector3.Lerp(m_Liquido.transform.localPosition, new Vector3(m_PosicionLiq.x, m_PosicionLiq.y - m_Redliquido, m_PosicionLiq.z), speed * Time.deltaTime);
        }
    }
}
