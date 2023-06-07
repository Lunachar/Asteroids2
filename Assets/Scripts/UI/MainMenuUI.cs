using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids2.UI
{
    public class MainMenuUI:MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void OpenSettings()
        {
            SceneManager.LoadScene("SettingsScene");
        }
    }
}