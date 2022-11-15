using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEscalable : MonoBehaviour
{
    public Valve valvulaIn, valvulaOut;
    public float actualLevel, relativeDensity = 1;
    public Transform waterPosition;
    // Start is called before the first frame updat
    private void FixedUpdate() {
        int alturaMax = GetComponent<TankResizable>().alturaCm;

        actualLevel += ((valvulaIn.status? 50:0)-(valvulaOut.status? 50:0))*Time.deltaTime*0.5f;

        if(actualLevel<0)
            actualLevel = 0;
        if(actualLevel> alturaMax)
            actualLevel = alturaMax;

        waterPosition.localScale = new Vector3(1,0.001f+actualLevel*0.01f,1);
    }
}
