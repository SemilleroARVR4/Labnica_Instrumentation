using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssistantScript : MonoBehaviour
{
    public Text mText;
    public List<string> mInteracciones;
    string error = "";

    // Start is called before the first frame update
    void Start()
    {
        mInteracciones.Add("Bienvenido a Labnica");
        mInteracciones.Add("Incorrecto error en:" + error);
        mInteracciones.Add("Correcto");

         mText.text = mInteracciones[0];
    }

}
