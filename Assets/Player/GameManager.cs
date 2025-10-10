//using NUnit.Framework.Interfaces;
//using TMPro;
//using UnityEngine;
//using UnityEngine.Events;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField] private GameInfo gameInfo;
//    [SerializeField] private PlayerController player;
//    [SerializeField] private UnityEvent startNewGame;
//    [SerializeField] private TMP_Text timer;
//    [SerializeField] private Vector3 spawnPosition;
//    [SerializeField] private AudioSource _backgroundMusic;

//    public bool updateTimer;
//    public bool endGame;
//    public EndGameScript endGameScript;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        // Temp
//        startNewGame.Invoke();
//    }

//    private void Update()
//    {
//        if (gameInfo.currentTimer <= 0)
//        {
//            endGame = true;
//            gameInfo.ResetValues();
//            gameEnded();
//        }
//        else
//        {
//            if (updateTimer)
//            {
//                gameInfo.currentTimer -= Time.deltaTime;
//                //timer.text = Mathf.Floor(gameInfo.currentTimer / 60) + ":" + (Mathf.Floor(gameInfo.currentTimer % 60 * 10) / 10).ToString();
//                timer.text = Mathf.Floor(gameInfo.currentTimer / 60) + ":" + Mathf.Floor(gameInfo.currentTimer % 60).ToString();
//            }
//        }
//    }

//    public void ToggleTimer(bool temp)
//    {
//        updateTimer = temp;
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//    }

//    public void UnpauseGame()
//    {
//        Time.timeScale = 1;
//    }

//    public void RestartGame()
//    {
//        updateTimer = false;
//        startNewGame.Invoke();
//        gameInfo.ResetValues();
//        endGame = false;
//        player.transform.position = spawnPosition;
//    }

//    private void gameEnded()
//    {
//        endGameScript.countNPCS();
//        endGameScript.countFood();
//        endGameScript.initDialogue();
//        endGameScript.gameObject.SetActive(true);
//        endGame = false;
//    }

//    public void playBGM()
//    {
//        _backgroundMusic.Play();
//    }

//    public void stopBGM()
//    {
//        _backgroundMusic.Stop();
//    }
//}

using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private PlayerController player;
    [SerializeField] private UnityEvent startNewGame;
    public bool endGame;
    public EndGameScript endGameScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Temp
        startNewGame.Invoke();
    }
    private void Update()
    {
        if (gameInfo.currentTimer <= 0)
        {
            endGame = true;
            gameInfo.ResetValues();
            gameEnded();
        }
        else
        {
            gameInfo.currentTimer -= Time.deltaTime;
        }
    }

    public void ResetValues()
    {
        startNewGame.Invoke();
        gameInfo.ResetValues();
        endGame = false;
    }

    private void gameEnded()
    {
        endGameScript.countNPCS();
        endGameScript.countFood();
        endGameScript.initDialogue();
        endGameScript.gameObject.SetActive(true);
        endGame = false;
    }
}
