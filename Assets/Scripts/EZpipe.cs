using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
[ExecuteInEditMode]
public class EZpipe : MonoBehaviour
{
    [Tooltip("Diametro del tubo en cm")]
    public float diametro = 0.1f;

    [Tooltip("Longitud")]
    public float longitud = 1;

    public bool isNormal;
    private void OnValidate() {
        transform.localScale = new Vector3(diametro, diametro, isNormal? longitud : diametro);
    }
}

[CustomEditor(typeof(EZpipe))]
public class TubosEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    private void OnMouseUp() {
        Debug.Log("OnMouseUp Editor");
    }
    public void OnSceneGUI()
    {
        var t = target as EZpipe;
        var tr = t.transform;
        Handles.color = new Color(0,0,255,0.5f);
        
        if(Handles.Button(tr.position+Vector3.up*0.5f,Quaternion.identity,0.2f,0.2f,Handles.CubeHandleCap))
            tr.eulerAngles += Vector3.forward*90;

        if(Event.current.type == EventType.MouseUp && Event.current.button == 0) {
            tr.GetChild(0).GetComponent<Collider>().enabled = false;
            tr.GetChild(1).GetComponent<Collider>().enabled = false;   
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;
            if( Physics.Raycast( ray, out hit,64,8) )
            { 
                tr.position = hit.collider.transform.position;
                tr.eulerAngles = hit.collider.transform.eulerAngles;
                var tubo = hit.collider.GetComponentInParent<EZpipe>();
                if(tubo)
                    t.diametro = tubo.diametro;
                    
                t.SendMessage("OnValidate");
            }
            tr.GetChild(0).GetComponent<Collider>().enabled = true;
            tr.GetChild(1).GetComponent<Collider>().enabled = true;
        }
    }
}

#endif