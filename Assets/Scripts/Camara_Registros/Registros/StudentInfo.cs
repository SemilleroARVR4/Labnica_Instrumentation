using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentInfo : MonoBehaviour
{
    public TxtController DBRegister;

    public string _studentName;
    public string _studentCode;
    public int _studentAge;
    public int _studentGenre;

    private void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        DBRegister = GameObject.FindGameObjectWithTag("DBRegister").GetComponent<TxtController>();
    }
}
