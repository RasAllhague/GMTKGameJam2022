using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMTKGameJam2022.Loading
{
    /// <summary>
    /// Data class for giving informations about a level.
    /// </summary>
    [Serializable]
    public class LevelInfo
    {
        [SerializeField]
        private int _sceneId;
        [SerializeField]
        private string _name;

        /// <summary>
        /// The build index id of the scene
        /// </summary>
        public int SceneId => _sceneId;
        /// <summary>
        /// The name of the level.
        /// </summary>
        public string LevelName => _name;
    }
}
