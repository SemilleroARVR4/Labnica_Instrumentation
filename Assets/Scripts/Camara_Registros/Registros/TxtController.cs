using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TxtController : MonoBehaviour
{
    public StudentInfo _studentRegister;
    public SessionInfo _sessionRegister;
    public CheckInfo   _checkRegister;

    public GameObject CharacterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //codigo que se tiene en cuenta a partir de la segunda ejecucion de la aplicacion, una vez se ha hecho el primer registro de estudiante.
        if (File.Exists(Application.dataPath + "/Resources/Estudiante.txt"))
        {
            string _lastStudentInfo = ReadLastStudentInfo();
            string[] _segmentedlastStudentInfo = _lastStudentInfo.Split(',');

            //Extraemos la informacion del estudiante para ser usada
            _studentRegister._studentCode = _segmentedlastStudentInfo[0];
            _studentRegister._studentName = _segmentedlastStudentInfo[1];
            _studentRegister._studentAge = int.Parse(_segmentedlastStudentInfo[2]);
            _studentRegister._studentGenre = int.Parse(_segmentedlastStudentInfo[3]);

            //Extraemos la informacion de la ultima sesion para registrar esta nueva session
            if (File.Exists(Application.dataPath + "/Resources/Sesion.txt"))
            {
                string _lastSessionInfo = ReadLastSessionInfo();
                string[] _segmentedlastSessionInfo = _lastSessionInfo.Split(',');
                _sessionRegister._sessionID = int.Parse(_segmentedlastSessionInfo[1]) + 1; //extrae del ultimo registro
            }

            //Inicializamos los datos del primer chequeo. Esto para crear la informacion de los primeros cables
            _checkRegister._sessionID = _sessionRegister._sessionID;
            _checkRegister._checkID = 0;

            //Registramos los datos de la sesion al iniciar la aplicacion.
            _studentRegister._studentCode = _segmentedlastStudentInfo[0]; //extraer del registor de estudiante
            _sessionRegister._studentID = _segmentedlastStudentInfo[0]; //extraer del registor de estudiante
            _sessionRegister._sessionStartTime = DateTime.Now;
            _sessionRegister._sessionEndTime = DateTime.MinValue;
            WriteSessionInfo();
        }
    }

    public void WriteStudentInfo()
    {

        if (!File.Exists(Application.dataPath + "/Resources/Estudiante.txt"))
        {
            try
            {
                //string fileName = Application.dataPath + "/Resources/Estudiante.txt";
                string fileName = Application.dataPath + "/Resources/Estudiante.txt";

                StreamWriter writer = File.AppendText(fileName);

                writer.WriteLine(/*_studentRegister._studentID + "," + */_studentRegister._studentCode.ToString() + "," + _studentRegister._studentName.ToString() + ","
                    + _studentRegister._studentAge.ToString() + "," + _studentRegister._studentGenre.ToString());
                writer.Close();
            }
            catch
            {
                Debug.Log("Error Estudiante");
            }
        }
    }

    public string ReadLastStudentInfo()
    {
        try
        {
            string fileName = Application.dataPath + "/Resources/Estudiante.txt";

            string[] reader = File.ReadAllLines(fileName);

            return reader[reader.Length - 1];
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public void WriteSessionInfo()
    {
        try
        {
            string fileName = Application.dataPath + "/Resources/Sesion.txt";

            StreamWriter writer = File.AppendText(fileName);

            writer.WriteLine(_studentRegister._studentCode.ToString() + "," + _sessionRegister._sessionID.ToString() + ","
                + _sessionRegister._sessionStartTime.ToString() + "," + _sessionRegister._sessionEndTime.ToString());

            writer.Close();
        }
        catch
        {
            Debug.Log("Error Sesión");
        }
    }

    public void RewriteSessionInfo()
    {
        int i = 0;
        StreamWriter writer = null;

        string _searchLine = _studentRegister._studentCode.ToString() + "," + _sessionRegister._sessionID.ToString() + ","
                + _sessionRegister._sessionStartTime.ToString() + "," + _sessionRegister._sessionEndTime.ToString();

        string _replaceLine = _studentRegister._studentCode.ToString() + "," + _sessionRegister._sessionID.ToString() + ","
                + _sessionRegister._sessionStartTime.ToString() + "," + DateTime.Now;

        try
        {
            string fileName = Application.dataPath + "/Resources/Sesion.txt";

            string[] reader = File.ReadAllLines(fileName);

            while (!reader[i].Equals(_searchLine))
            {
                i++;
            }

            reader[i] = _replaceLine;

            writer = new StreamWriter(fileName);

            foreach (string line in reader)
            {
                writer.WriteLine(line);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            writer.Close();
        }
    }

    public string ReadLastSessionInfo()
    {
        try
        {
            string fileName = Application.dataPath + "/Resources/Sesion.txt";

            string[] reader = File.ReadAllLines(fileName);

            return reader[reader.Length - 1];
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public void WriteCheckInfo()
    {
        try
        {
            string fileName = Application.dataPath + "/Resources/Verificacion.txt";

            StreamWriter writer = File.AppendText(fileName);

            writer.WriteLine(_sessionRegister._sessionID.ToString() + "," + _checkRegister._checkID.ToString() + ","
                + _checkRegister._checkTime.ToString() + "," + _checkRegister._closedLoop);

            writer.Close();
        }
        catch
        {
            Debug.Log("Error CheckInfo");
        }
    }

    public string ReadLastCheckInfo()
    {
        try
        {
            string fileName = Application.dataPath + "/Resources/Verificacion.txt";

            string[] reader = File.ReadAllLines(fileName);

            return reader[reader.Length -1 ];
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
    
    public void WriteCableInfo(CableInfo _cableinfo)
    {
        try
        {
            string fileName = Application.dataPath + "/Resources/Cables.txt";

            StreamWriter writer = File.AppendText(fileName);

            writer.WriteLine(_cableinfo._sessionID.ToString() + "," + _cableinfo._checkID.ToString() + "," + _cableinfo._cableID.ToString()
                + "," + _cableinfo._startTimeCable.ToString() + "," + _cableinfo._deleteTimeCable.ToString() + "," + _cableinfo._deviceTouched1.ToString()
                + "," + _cableinfo._deviceTouched2.ToString() + "," + _cableinfo._connectorTouched1.ToString() + "," + _cableinfo._connectorTouched2.ToString()
                + "," + _cableinfo._cableState);

            writer.Close();
        }
        catch
        {
            Debug.Log("Error WriteCableInfo");
        }
    }

    public void ResetCamera()
    {
        Destroy(Camera.main.gameObject.transform.parent.gameObject);
        GameObject character = Instantiate(CharacterPrefab, new Vector3(0, 0, 3.3f), CharacterPrefab.transform.rotation);        
    }
}
