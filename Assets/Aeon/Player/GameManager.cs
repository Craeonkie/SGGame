using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private PlayerController player;
    [SerializeField] private UnityEvent startNewGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Temp
        startNewGame.Invoke();
    }

    private void Update()
    {
        gameInfo.currentTimer -= Time.deltaTime;
    }

    public void ResetValues()
    {
        startNewGame.Invoke();
        gameInfo.ResetValues();
    }
}
