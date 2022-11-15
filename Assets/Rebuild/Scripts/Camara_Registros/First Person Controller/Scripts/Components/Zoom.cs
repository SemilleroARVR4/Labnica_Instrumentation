using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1f;
    private bool stateZoom = false;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && stateZoom == false)
        {
            currentZoom = 1;
            stateZoom = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && stateZoom == true)
        { 
            currentZoom = 0;
            stateZoom = false;
        }

        // Update the currentZoom and the camera's fieldOfView.
        //currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
