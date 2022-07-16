using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void SwitchScene(int sceneNumber)
    {
        if(sceneNumber == 99)
        {
            Application.Quit();
        }
        SceneManager.LoadScene(sceneNumber);
    }
}
