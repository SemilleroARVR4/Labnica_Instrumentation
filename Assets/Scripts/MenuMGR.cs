using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMGR : MonoBehaviour
{
    public void LoadIndividual()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EscenaMultiplayer");
    }
}
