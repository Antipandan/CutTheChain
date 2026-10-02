using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    // Unity events dont accept static function in the inspector fully. This is a work around for that
    public class GameFunctions : MonoBehaviour
    {
        private static GameFunctions instance;

        private void Awake()
        {
            CheckSingleton();
        }

        private void CheckSingleton()
        {
            if (instance == null) instance = this;
            else Destroy(this);
        }

        public static void PauseGame()
        {
            Debug.Log($"game pause!");
            GameEvents.Instance.PublishOnGamePaused();
            Time.timeScale = 0f;
        }

        public static void ResumeGame()
        {
            Debug.Log($"game resume!");
            GameEvents.Instance.PublishOnGameResumed();
            Time.timeScale = 1f;
        }
    
        public static void ChangeTimeScaleManually(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        /// <summary>
        /// Switches between Time.timeScale 1f and 0f
        /// </summary>
        public static void SwitchPauseResume()
        {
            bool result = Time.timeScale > 0f ^ true;
            if (result) ResumeGame();
            else PauseGame();
        }

        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static void LoadScene(string sceneName)
        {
            if (int.TryParse(sceneName, out int sceneNumber))
            {
                TryLoadSceneByNumber(sceneNumber);
            }
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadScene(int sceneNumber)
        {
            TryLoadSceneByNumber(sceneNumber);
        }

        private static void TryLoadSceneByNumber(int sceneNumber)
        {
            if (sceneNumber < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(sceneNumber);
            }
        }
    }
}