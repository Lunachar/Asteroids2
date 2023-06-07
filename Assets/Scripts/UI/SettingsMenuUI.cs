using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids2.UI
{
    public class SettingsMenuUI:MonoBehaviour
    {
        public void AdjustVolume(float Volume)
        {
            
        }

        public void ReturnToGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}