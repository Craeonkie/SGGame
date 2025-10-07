using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField] private bool playerInRange = false;
    [SerializeField] private TagHandle playerTag;
    private List<Collider> _hitColliders;

    private void Start()
    {
        _hitColliders = new();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _hitColliders.Add(other);
            ScanArea();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _hitColliders.Add(other);
            ScanArea();
        }
    }

    void ScanArea()
    {
        if (_hitColliders.Count != 0)
        {
            playerInRange = false;
        }
        else
        {
            playerInRange = true;
        }
    }
}
