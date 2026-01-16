using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private PlayerController player;
    [SerializeField] private UnityEvent startNewGame;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private AudioSource _backgroundMusic;

    public bool updateTimer;
    public EndGameScript endGameScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startNewGame.Invoke();
        string seconds = Mathf.Floor(gameInfo.currentTimer % 60).ToString();
        if (seconds.Length < 2)
        {
            seconds = '0' + seconds;
        }
        timer.text = Mathf.Floor(gameInfo.currentTimer / 60) + ":" + seconds;
    }

    private void Update()
    {
        if (gameInfo.currentTimer <= 0)
        {
            EndGame();
        }
        else
        {
            if (updateTimer)
            {
                gameInfo.currentTimer -= Time.deltaTime;
                //timer.text = Mathf.Floor(gameInfo.currentTimer / 60) + ":" + (Mathf.Floor(gameInfo.currentTimer % 60 * 10) / 10).ToString();
                string seconds = Mathf.Floor(gameInfo.currentTimer % 60).ToString();
                if (seconds.Length < 2)
                {
                    seconds = '0' + seconds;
                }
                timer.text = Mathf.Floor(gameInfo.currentTimer / 60) + ":" + seconds;
            }
        }
    }

    public void ToggleTimer(bool temp)
    {
        updateTimer = temp;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        updateTimer = false;
        startNewGame.Invoke();
        gameInfo.ResetValues();
        string seconds = Mathf.Floor(gameInfo.currentTimer % 60).ToString();
        if (seconds.Length < 2)
        {
            seconds = '0' + seconds;
        }
        timer.text = Mathf.Floor(gameInfo.currentTimer / 60) + ":" + seconds;
        player.transform.position = spawnPosition;
    }

    public void EndGame()
    {
        endGameScript.TriggerEnd();
    }

    public void PlayBGM()
    {
        _backgroundMusic.Play();
    }

    public void StopBGM()
    {
        _backgroundMusic.Stop();
    }
}