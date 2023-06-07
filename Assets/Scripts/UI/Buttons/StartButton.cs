namespace UI.Buttons
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}