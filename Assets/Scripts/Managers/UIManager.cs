using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTKGameJam2022.Managers
{
    /// <summary>
    /// Class for managing consistent game ui stuff like main menus and UI states. Just a template for now.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

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