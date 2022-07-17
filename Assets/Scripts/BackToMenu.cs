using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{ 
    public void BackToMenuClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // Load Menu
    }
}
