using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableMouseController : MonoBehaviour
{
    private Vector3 screenPoint;
    CableLine line;

    private void Awake() {
        line = GetComponentInParent<CableLine>();
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        CursorManager.SetGrabCursor();
    }

    private void OnMouseUp()
    {
        CursorManager.SetNormalCursor();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray,0.02f, out RaycastHit hit, 5, (1<<7)))
        {
            print("Ray to Connector");
            transform.position = hit.transform.position - hit.transform.forward*0.05f;
            transform.rotation = hit.transform.rotation;
            SendMessageUpwards("UpdateLine");
        }
    }


    private void OnMouseDrag()
    {
        if(line.canMove)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
            line.UpdateLine();
        }
        else
            MessageBox.ShowMessage("Error","No puedes desconectar cables con la fuente encendida");
    }
}
