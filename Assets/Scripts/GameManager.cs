using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxScore = 10;
    public static GameManager Instance;
    public GameState State;
    public Ball ball;
 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameStatus;
    public RectTransform gameOverPanel;

    private int _playerScore = 0;

    public TextMeshProUGUI timmer; 


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


    }
    private void Start()
    {

        UpdateGameState(GameState.Restart);

    }

    IEnumerator CountDownTimmer(int counter)
    {
        timmer.text = counter.ToString();
        counter--;
        if(counter < 0)
        {
            StopCoroutine(CountDownTimmer(0));
            timmer.gameObject.SetActive(false);
            UpdateGameState(GameState.PlayMode);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            yield return StartCoroutine(CountDownTimmer(counter));
        }
       
    }

    public void UpdateScore()
    {
        _playerScore ++;
        scoreText.text = _playerScore.ToString();

        if (_playerScore >= maxScore)
        {
            UpdateGameState(GameState.Victory);
        }
    }
    public void FinalScoreStatus(int score , bool isWon)
    {
        Time.timeScale = 0;
        gameOverPanel.gameObject.SetActive(true);
        gameStatus.text = isWon ? "YOU WON" : "YOU LOST";

    }
    public void RestartButton()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.SelectionScreen:
                break;
            case GameState.PlayMode:
                ball.ResetPosition();
                break;
            case GameState.GamePaused:
                break;
            case GameState.Victory:
                FinalScoreStatus(_playerScore, true);
                break;
            case GameState.Lose:
                FinalScoreStatus(_playerScore, false);
                break;
            case GameState.GameEnd:

                break;
            case GameState.Restart:
                StartCoroutine(CountDownTimmer(5));
                timmer.gameObject.SetActive(true);
                Time.timeScale = 1;
                _playerScore = 0;
                scoreText.text = _playerScore.ToString();
                gameOverPanel.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
public enum GameState
{
    SelectionScreen,
    Restart,
    PlayMode,
    GamePaused,
    Victory,
    Lose,
    GameEnd
}