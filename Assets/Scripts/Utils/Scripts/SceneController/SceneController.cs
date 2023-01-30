using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewUtils
{
    public class SceneController : DontDestroySingleton<SceneController>
    {
        public int CurrentLevel { get; private set; }

        private void Awake()
        {
            Init();
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel_", 1);
            SceneManager.LoadScene(CurrentLevel);
        }

        public void NextLevel()
        {
            if (CurrentLevel + 1 >= SceneManager.sceneCountInBuildSettings) return;

            CurrentLevel += 1;
            SceneManager.LoadScene(CurrentLevel);
        }

        private void OnApplicationPause(bool pause)
        {
            if (!pause) return;

            PlayerPrefs.SetInt("CurrentLevel_", CurrentLevel);
        }
    }
}