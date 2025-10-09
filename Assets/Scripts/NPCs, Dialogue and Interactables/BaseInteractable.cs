using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour
{
    [SerializeField] protected UnityEvent whenInteractedWith;
    protected bool interacted;

    public virtual void GetInteractedWith()
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
