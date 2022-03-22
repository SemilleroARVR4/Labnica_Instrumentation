using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableStartPositions : MonoBehaviour
{
    [SerializeField] GameObject startPosition1;
    [SerializeField] GameObject startPosition2;
    [SerializeField] GameObject CablestartPosition1;
    [SerializeField] GameObject CablestartPosition2;

    // Start is called before the first frame update
    void Start()
    {
        CablestartPosition1.transform.position = startPosition1.transform.position;
        CablestartPosition1.transform.rotation = startPosition1.transform.rotation;
        CablestartPosition2.transform.position = startPosition2.transform.position;
        CablestartPosition2.transform.rotation = startPosition2.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
