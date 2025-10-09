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
