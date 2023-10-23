using UnityEngine;

namespace Asteroids2.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public PauseManager PauseManager;

        public void OpenSettings()
        {
            gameObject.SetActive(true);
            PauseManager.TogglePause();
        }

        public void CloseSettings()
        {
            gameObject.SetActive(false);
            PauseManager.TogglePause();
        }

    }
}