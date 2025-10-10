using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private Material _particleMat;
    [SerializeField] private GameObject _particleGameObj;

    public void onDirt()
    {
        _particleMat.color = new Color(.5f, .2f, .2f);
    }

    public void onGrass()
    {
        _particleMat.color = new Color(.4f, .6f, .0f);
    }

    public void onMud()
    {
        _particleMat.color = new Color(.8f, .5f, .0f);
    }
    public void onWater()
    {
        _particleMat.color = new Color(.0f, .5f, .5f);
    }

    public void flipParticle(float y)
    {
        var particleSys = _particleGameObj.GetComponent<ParticleSystem>().shape;
        particleSys.rotation = new Vector3(0, y, 0);
    }
}