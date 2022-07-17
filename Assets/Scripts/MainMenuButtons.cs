using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public void SwitchScene(int sceneNumber)
    {
        if (sceneNumber < 99)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            Application.Quit();
        }
    }
}
