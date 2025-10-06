using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PlayerController _playerController;
    private List<Collider> _hitColliders;

    private void Start()
    {
        _hitColliders = new();
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            _hitColliders.Add(other);
            ScanArea();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            _hitColliders.Remove(other);
            ScanArea();
        }
    }

    void ScanArea()
    {
        if (_hitColliders.Count != 0)
        {
            _playerController.isGrounded = true;
        }
        else
        {
            _playerController.isGrounded = false;
        }
    }
}