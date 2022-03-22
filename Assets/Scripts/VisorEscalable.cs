using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class VisorEscalable : MonoBehaviour
{
    public TankResizable attachedTank;
    public int Escala;
    public TextMeshPro txtScale;
    public Transform fondoEscala, vidrio, liquido;

    private void Update() {
        Escala = attachedTank.alturaCm;
        if(fondoEscala.localScale.y != Escala)
        {
            fondoEscala.localScale = new Vector3(1,(5+Escala)*0.01f,1);
            vidrio.localScale = new Vector3(1,(5+Escala)*0.01f,1);
        }
        txtScale.text = "";
        for(int i = (Escala/5)*5; i>=0 ; i-=5)
            txtScale.text += $"{i}---\n";
        float actualLevel = GetComponentInParent<TankEscalable>().actualLevel;
        liquido.localScale = new Vector3(1,0.001f+actualLevel*0.01f,1);
    }
}
