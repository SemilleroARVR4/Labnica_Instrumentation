using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLine : MonoBehaviour
{
    public Transform start, end;
    LineRenderer line;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
    }

    private void OnValidate() {
        line = GetComponentInChildren<LineRenderer>();
        UpdateLine();
    }

    // Update is called once per frame
    public void UpdateLine()
    {
        line.SetPosition(0,start.position);
        line.SetPosition(1,end.position);
    }
}
