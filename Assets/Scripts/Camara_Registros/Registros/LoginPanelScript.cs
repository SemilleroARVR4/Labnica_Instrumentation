using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelScript : MonoBehaviour
{
    [SerializeField] TxtController _txtController;
    [SerializeField] InputField _ifName;
    [SerializeField] InputField _ifCode;
    [SerializeField] InputField _ifAge;
    [SerializeField] Dropdown _ifGenre;

    [SerializeField] GameObject _btnVolver;

    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _AuxiliaryCameras;
    [SerializeField] GameObject _Assistant;

    public GameObject _circuitDiagram;
    public GameObject _ControllerImage;

    private bool _stateViewDiagram = false;
    private bool _stateViewController = false;
    private bool _stateViewCamera = false;
    private bool _stateViewAssistant = false;


    private void Start()
    {
        if (File.Exists(Application.dataPath + "/Resources/Estudiante.txt"))
            ClosePanel();
        else
        {
            gameObject.SetActive(true);
            _btnVolver.SetActive(false);
        }
    }

    public void RegisterUserProfile()
    {
        try
        {
            //Guardamos informacion del estudiante en el primer registro
            _txtController._studentRegister._studentName = _ifName.text;
            _txtController._studentRegister._studentCode = _ifCode.text;

            _txtController._studentRegister._studentAge = int.Parse(_ifAge.text);
            _txtController._studentRegister._studentGenre = _ifGenre.value;

            //Guardamos informacion de la primera sesion
            _txtController._sessionRegister._studentID = _ifCode.text;
            _txtController._sessionRegister._sessionID = 1;
            _txtController._sessionRegister._sessionStartTime = System.DateTime.Now;
            _txtController._sessionRegister._sessionEndTime = System.DateTime.MinValue;

            //Actualizamos informacion del primer chequeo
            _txtController._checkRegister._sessionID = _txtController._sessionRegister._sessionID;

            //Escribimos los datos del estudiante y la sesion la primera vez que se ejecuta la aplicacion
            _txtController.WriteStudentInfo();
            _txtController.WriteSessionInfo();

            _btnVolver.SetActive(true);
            ClosePanel();
        }
        catch
        {
            Debug.Log("Error al ingresar datos");
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        _gamePanel.SetActive(true);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void ViewCircuitDiagram()
    {
        _stateViewDiagram = !_stateViewDiagram;
        _circuitDiagram.SetActive(_stateViewDiagram);
    }

    public void ViewController()
    {
        _stateViewController = !_stateViewController;
        _ControllerImage.SetActive(_stateViewController);
    }

    public void QuitApplicationButton()
    {
        _txtController.RewriteSessionInfo();
        Application.Quit();
    }

    public void ViewAuxiliaryCamera() 
    {
        _stateViewCamera = !_stateViewCamera;
        _AuxiliaryCameras.SetActive(_stateViewCamera);
    }

    public void ViewAassistant() 
    {
        _stateViewAssistant = !_stateViewAssistant;
        _Assistant.SetActive(_stateViewAssistant);

    }


}
