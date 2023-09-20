namespace UI.Buttons
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
            }
        }
    }
}