using GMTKGameJam2022.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTKGameJam2022.Loading
{
    /// <summary>
    /// This shows how you can use the <see cref="LevelLoader"/> to load another level.
    /// </summary>
    public class SceneSwitchTrigger : MonoBehaviour
    {
        /// <summary>
        /// The level loader reference of this scene
        /// </summary>
        [SerializeField]
        private LevelLoader _loader;
        /// <summary>
        /// The info about the level you are switching to.
        /// </summary>
        [SerializeField]
        private LevelInfo _levelInfo;

        /// <summary>
        /// This is bad, but simple than adding a player and making the stuff needed for event dispatching.
        /// </summary>
        private void FixedUpdate()
        {
            var gamepad = Gamepad.current;
            if (gamepad == null)
                return;

            if (gamepad.aButton.IsPressed())
            {
                _loader.LoadNextLevel(_levelInfo);
            }
        }
    }
}