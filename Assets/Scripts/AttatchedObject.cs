using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AttatchedObject : MonoBehaviour
{
    public Transform baseTransform;
    Vector3 offset;
    public bool isReady;


    private void Awake() {
        offset = transform.position - baseTransform.position;
    }

    private void OnValidate() {
        if(isReady)
            offset = transform.position - baseTransform.position;
    }

    public void AdjustCable()
    {
        transform.position = baseTransform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
    if(isReady)
        transform.position = baseTransform.position + offset;
#endif
    }
}
