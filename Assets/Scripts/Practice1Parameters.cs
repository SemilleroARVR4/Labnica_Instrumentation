using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Practice1Parameters : MonoBehaviourPun
{
    public PracticeParameter heightParam, radiusParam, txHeightParam, pressureTakeParam, densityParam;
    //heightParam, radiusParam, txHeightParam, pressureTakeHeightParam, densityParam;
    public static float[] parameters;

    public void SetParameters()
    {
        parameters = new float[5];
        parameters[0] = heightParam.actualValue;
        parameters[1] = radiusParam.actualValue;
        parameters[2] = txHeightParam.actualValue;
        parameters[3] = pressureTakeParam.actualValue;
        parameters[4] = densityParam.actualValue;
    }

    public void SendParameters(Player player)
    {
        photonView.RPC(nameof(GetPracticeParameters),player, parameters);
    }

    [PunRPC]
    public void GetPracticeParameters(float[] HostParameters)
    {
        parameters = HostParameters;
        FindObjectOfType<Practice1Preview>().SetParameters(parameters);
    }
}
