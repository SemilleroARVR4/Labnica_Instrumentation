using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryCamera : MonoBehaviour
{
    public GameObject mCamara1;
    public GameObject mCamara2;
    public GameObject mperson;

    // Update is called once per frame
    private void Start() {
        mperson = FindObjectOfType<FirstPersonMovement>().gameObject;
    }

    private void FixedUpdate()
    {
        if (mperson.transform.position.x > -1.5f && mperson.transform.position.x < -0.5f && mperson.transform.position.z > 3f )
        {
            mCamara1.SetActive(false);
            mCamara2.SetActive(true);
        }
        else if (mperson.transform.position.x > -0.5f && mperson.transform.position.x < 0.5f && mperson.transform.position.z > 3f)
        {
            mCamara1.SetActive(true);
            mCamara2.SetActive(false);
        }
        else 
        {
            mCamara1.SetActive(false);
            mCamara2.SetActive(false);
        }
    }

}
