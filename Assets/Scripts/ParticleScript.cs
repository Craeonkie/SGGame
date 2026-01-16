using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private Material _particleMat;
    [SerializeField] private GameObject _particleGameObj;
    private bool _moving;
    private bool _onAnything;

    public void Moving(bool isMoving)
    {
        _moving = isMoving;
    }

    public void OnDirt()
    {
        _particleMat.color = new Color(.5f, .2f, .2f);
        _onAnything = true;
    }

    public void OnGrass()
    {
        _particleMat.color = new Color(.4f, .6f, .0f);
        _onAnything = true;
    }
    public void OnMud()
    {
        _particleMat.color = new Color(.8f, .5f, .0f);
        _onAnything = true;
    }
    public void OnWater()
    {
        _particleMat.color = new Color(.0f, .5f, .5f);
        _onAnything = true;
    }
    public void OnNothing()
    {
        _onAnything = false;
    }

    private void Update()
    {
        if (_onAnything && _moving)
        {
            _particleGameObj.SetActive(true);
        }
        else
        {
            _particleGameObj.SetActive(false);
        }
    }

    public void FlipParticle(float y)
    {
        var particleSys = _particleGameObj.GetComponent<ParticleSystem>().shape;
        particleSys.rotation = new Vector3(0, y, 0);
    }
}