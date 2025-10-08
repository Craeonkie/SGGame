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
        ResetValues();
    }

    public void EnterDialogue()
    {
        player.inMenu = true;
    }

    public void ResetValues()
    {
        startNewGame.Invoke();
    }
}
