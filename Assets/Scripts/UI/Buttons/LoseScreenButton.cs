using Asteroids2;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenButton : MonoBehaviour
{
    public Button backToGame;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("ManagersDDOL").GetComponent<GameManager>();
        backToGame.onClick.AddListener(BackToGame);
    }
    
    private void BackToGame()
    {
        StartCoroutine(_gameManager.StartMainMenu());
    }

}
