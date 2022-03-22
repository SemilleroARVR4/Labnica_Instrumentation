using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisorTank : MonoBehaviour
{
    public Tank tank;
    public Transform linelvl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float escala = tank.nivel/130;
        escala = escala == 0? 0.001f: escala;
        linelvl.localScale = new Vector3(1, escala,1);
    }
}
