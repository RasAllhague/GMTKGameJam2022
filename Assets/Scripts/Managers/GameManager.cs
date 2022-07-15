using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTKGameJam2022.Managers
{
    /// <summary>
    /// Gamemanager singleton. Put all important stuff for game managing here.
    /// </summary>
    public class GameManager : MonoBehaviour
    { 
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
        
    }
}