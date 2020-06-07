using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Utils.CheckIfGameObjectIsNull(_gameManager);
    }

    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void updateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence() 
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(Blink());
        _gameManager.GameOver();
    }

    public void showGameOver()
    {
        
    }

    IEnumerator Blink()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            _restartText.text = "Press 'R' to restart!";
            yield return new WaitForSeconds(0.7f);
            _gameOverText.text = "";
            _restartText.text = "";
            yield return new WaitForSeconds(0.7f);
        }
    }
}
