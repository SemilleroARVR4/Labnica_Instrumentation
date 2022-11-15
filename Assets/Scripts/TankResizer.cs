using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankResizer : MonoBehaviour
{
    public float  legHeight = 1, height = 1, radius = 0.5f;
    // Start is called before the first frame update
    public Transform armature, tankBase, heighBone;


    public void OnValidate() {
        armature.localScale = new Vector3(radius*2,radius*2,1);
        tankBase.localPosition = Vector3.up*legHeight;
        heighBone.localPosition = Vector3.up*height;
    }
}
