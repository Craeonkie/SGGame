using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "Scriptable Objects/GameInfo")]
public class GameInfo : ScriptableObject
{
    [Header("Base Values")]
    [SerializeField] private float maxTimer;
    //[SerializeField] private float someinventorychecklistshit;

    [Header("Changing Values")]
    public float currentTimer;

    public void UpdateValues(float dt)
    {
        currentTimer -= dt;
    }

    public void ResetValues()
    {
        currentTimer = maxTimer;
    }
}
