using GMTKGameJam2022.Loading.Exceptions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GMTKGameJam2022.Loading
{
    /// <summary>
    /// Simple level loader script. Needs to be added to every scene.
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        /// <summary>
        /// The animator for the transitions.
        /// </summary>
        [SerializeField]
        private Animator _transition;
        /// <summary>
        /// The canvas which contains the loading screen ui elements.
        /// </summary>
        [SerializeField]
        private Canvas _loadingScreen;
        /// <summary>
        /// The time for the transition in the couroutine.
        /// </summary>
        [SerializeField]
        private float _transitionTime = 0;

        /// <summary>
        /// Loads the next level based on the current level index.
        /// </summary>
        public void LoadNextLevel(LevelInfo level)
        {
            if (SceneManager.GetSceneByBuildIndex(level.SceneId) == null)
            {
                throw new LevelLoadingException("Cannot find the specified scene in scene build index.");
            }

            StartCoroutine(LoadLevel(level));
        }

        /// <summary>
        /// Loads a level.
        /// </summary>
        /// <param name="info">Information about the level as well as about the transition.</param>
        /// <returns></returns>
        private IEnumerator LoadLevel(LevelInfo info)
        {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(info.SceneId);
        }
    }
}