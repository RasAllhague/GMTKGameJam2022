using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public int currentLevelID = 1;
    public void BackToMenuClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // Load Menu
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;

        int nextLevelID = 0;

        switch (currentLevelID)
        {
            case 1:
                nextLevelID = 2;
                break;
            case 2:
                nextLevelID = 3;
                break;
            case 3:
                nextLevelID = 4;
                break;
            case 4:
                nextLevelID = 5;
                break;
            case 5:
                nextLevelID = 6;
                break;
            case 6:
                nextLevelID = 0;
                break;
        }

        SceneManager.LoadScene(nextLevelID);
    }
}
