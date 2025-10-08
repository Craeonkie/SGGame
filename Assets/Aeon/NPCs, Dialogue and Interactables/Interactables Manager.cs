using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    [SerializeField] private List<BaseInteractable> baseInteractables;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ResetValues()
    {
        foreach (BaseInteractable baseInteractable in baseInteractables)
        {
            baseInteractable.ResetValues();
        }
    }
}
