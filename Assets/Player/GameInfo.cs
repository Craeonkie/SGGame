//using System;
//using UnityEngine;

//[CreateAssetMenu(fileName = "GameInfo", menuName = "Scriptable Objects/GameInfo")]
//public class GameInfo : ScriptableObject
//{
//    [Header("Base Values")]
//    [SerializeField] private float maxTimer;
//    //public bool _runTimer;
//    //[SerializeField] private float someinventorychecklistshit;

//    [Header("Changing Values")]
//    public float currentTimer;

//    public void ResetValues()
//    {
//        currentTimer = maxTimer;
//    }

//    internal bool UpdateValues(object dt)
//    {
//        throw new NotImplementedException();
//    }
//}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "Scriptable Objects/GameInfo")]
public class GameInfo : ScriptableObject
{
    [Header("Base Values")]
    [SerializeField] private float maxTimer;
    //public bool _runTimer;
    //[SerializeField] private float someinventorychecklistshit;

    [Header("Changing Values")]
    public float currentTimer;

    //public void UpdateValues(float dt)
    //{
    //    if (_runTimer)
    //        currentTimer -= dt;
    //    else
    //        ResetValues();
    //}

    public void ResetValues()
    {
        currentTimer = maxTimer;
    }

    internal bool UpdateValues(object dt)
    {
        throw new NotImplementedException();
    }
}
