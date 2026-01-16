using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private Material _particleMat;
    [SerializeField] private ParticleSystem _particleGameObj;
    private bool _moving;
    private bool _onAnything;

    public void Moving(bool isMoving)
    {
        _moving = isMoving;
    }

    public void OnDirt()
    {
        var _particleColor = _particleGameObj.main;
        _particleColor.startColor = new Color(.5f, .2f, .2f);
        _onAnything = true;
    }

    public void OnGrass()
    {
        var _particleColor = _particleGameObj.main;
        _particleColor.startColor = new Color(.4f, .6f, .0f);
        _onAnything = true;
    }
    public void OnMud()
    {
        var _particleColor = _particleGameObj.main;
        _particleColor.startColor = new Color(.8f, .5f, .0f);
        _onAnything = true;
    }
    public void OnWater()
    {
        var _particleColor = _particleGameObj.main;
        _particleColor.startColor = new Color(.0f, .5f, .5f);
        _onAnything = true;
    }
    public void OnNothing()
    {
        _onAnything = false;
    }

    private void Update()
    {
        var particleSys = _particleGameObj.emission;

        if (_onAnything && _moving)
        {
            particleSys.enabled = true;
        }
        else
        {
            particleSys.enabled = false;
        }
    }

    public void FlipParticle(float y)
    {
        var particleSys = _particleGameObj.shape;
        particleSys.rotation = new Vector3(0, y, 0);
    }
}