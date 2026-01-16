using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static PlayerController;

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
            UpdateGroundState();
        }
        else
        {
            _playerController.isGrounded = false;
        }
    }

    void UpdateGroundState()
    {
        foreach (var col in _hitColliders)
        {
            if (col.CompareTag("Water"))
            {
                if (_playerController.standingOn != SurfaceType.Water)
                {
                    _playerController.audioSource.PlayOneShot(_playerController.audioClip[2]);
                    _playerController.standingOn = SurfaceType.Water;
                }
                else
                {
                    _playerController.audioSource.PlayOneShot(_playerController.audioClip[3]);
                }
                break;
            }
            else if (col.CompareTag("Mud"))
            {
                _playerController.standingOn = SurfaceType.Mud;
            }
            else if (col.CompareTag("Grass"))
            {
                if (_playerController.standingOn != SurfaceType.Mud)
                {
                    _playerController.standingOn = SurfaceType.Grass;
                }
            }
            else if (col.CompareTag("Path"))
            {
                if (_playerController.standingOn != SurfaceType.Mud && _playerController.standingOn != SurfaceType.Grass)
                {
                    _playerController.standingOn = SurfaceType.Dirt;
                }
            }
            else
            {
                _playerController.standingOn = SurfaceType.None;
            }
        }

        if (_playerController.standingOn == SurfaceType.Mud)
        {
            _playerController.audioSource.PlayOneShot(_playerController.audioClip[1]);
        }
        else if (_playerController.standingOn == SurfaceType.Grass)
        {

        }
        else if (_playerController.standingOn == SurfaceType.Dirt)
        {

        }
    }
}