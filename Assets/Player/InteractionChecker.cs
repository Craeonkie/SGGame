//using System.Collections.Generic;
//using UnityEngine.InputSystem;
//using UnityEngine;

//public class InteractionChecker : MonoBehaviour
//{
//    [SerializeField] private string tagName;
//    private List<Collider> _hitColliders;
//    private bool canInteract;

//    private void Start()
//    {
//        canInteract = true;
//        _hitColliders = new();
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag(tagName))
//        {
//            _hitColliders.Add(other);
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag(tagName))
//        {
//            _hitColliders.Add(other);
//        }
//    }

//    private void Interact(InputAction.CallbackContext ctx)
//    {
//        if (_hitColliders.Count != 0 && canInteract)
//        {
//            Collider closestCollider = null;
//            float minDistance = Mathf.Infinity;

//            foreach (Collider collider in _hitColliders)
//            {
//                if (collider == null) continue; // Skip null entries in the list

//                // Calculate the closest point on the current collider to the reference position
//                float distance = Vector3.Distance(transform.position, collider.transform.position);

//                if (distance < minDistance)
//                {
//                    minDistance = distance;
//                    closestCollider = collider;
//                }
//            }

//            if (closestCollider.TryGetComponent<BaseInteractable>(out var temp))
//            {
//                temp.GetInteractedWith();
//            }
//            else
//            {
//                Debug.Log("Trying to interact with a non Base Interactable! Are you sure you added the right script or tag?");
//            }
//        }
//    }

//    public void EnableInteracting()
//    {
//        canInteract = true;
//    }

//    public void DisableInteracting()
//    {
//        canInteract = false;
//    }

//    private void OnEnable()
//    {
//        var map = InputSystem.actions;
//        map.Enable();
//        map.FindAction("Interact").performed += Interact;
//    }

//    private void OnDisable()
//    {
//        var map = InputSystem.actions;
//        var move = map.FindAction("Interact");
//        if (move != null)
//        {
//            move.performed -= Interact;
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InteractionChecker : MonoBehaviour
{
    [SerializeField] private string tagName;
    private List<Collider> _hitColliders;
    private bool canInteract;

    private void Start()
    {
        canInteract = true;
        _hitColliders = new();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagName))
        {
            _hitColliders.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagName))
        {
            _hitColliders.Add(other);
        }
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (_hitColliders.Count != 0 && canInteract)
        {
            Collider closestCollider = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider collider in _hitColliders)
            {
                if (collider == null) continue; // Skip null entries in the list

                // Calculate the closest point on the current collider to the reference position
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCollider = collider;
                }
            }

            if (closestCollider.TryGetComponent<BaseInteractable>(out var temp))
            {
                temp.GetInteractedWith();
            }
            else
            {
                Debug.Log("Trying to interact with a non Base Interactable! Are you sure you added the right script or tag?");
            }
        }
    }

    public void EnableInteracting()
    {
        canInteract = true;
    }

    public void DisableInteracting()
    {
        canInteract = false;
    }

    private void OnEnable()
    {
        var map = InputSystem.actions;
        map.Enable();
        map.FindAction("Interact").performed += Interact;
    }

    private void OnDisable()
    {
        var map = InputSystem.actions;
        var move = map.FindAction("Interact");
        if (move != null)
        {
            move.performed -= Interact;
        }
    }
}