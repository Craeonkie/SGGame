using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour
{
    [SerializeField] private UnityEvent whenInteractedWith;
    private bool interacted;

    public void DoEvent()
    {
        if (interacted == false)
        {
            whenInteractedWith.Invoke();
        }
        interacted = true;
    }

    public virtual void ResetValues()
    {
        interacted = false;
    }
}
