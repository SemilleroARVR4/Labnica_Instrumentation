/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody r;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        r = gameObject.GetComponent<Rigidbody>();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (gameObject.GetComponent<ConnectCable>().canMove)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Input.mousePosition;
                pos.z = mZCoord;
                pos = GetMouseWorldPos();
                r.velocity = (pos - gameObject.transform.position) * 10;
            }
            if (Input.GetMouseButtonUp(0))
            {
                r.velocity = Vector3.zero;
            }
        }
        else
        {
            r.velocity = Vector3.zero;
        }
        //transform.position = GetMouseWorldPos() + mOffset;
    }
    private void OnMouseUp()
    {
        r.velocity = Vector3.zero;
    }
}
*/