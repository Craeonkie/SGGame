using UnityEngine;

public class RotateTowardsObject : MonoBehaviour
{
    [SerializeField] private Transform gameObjectTransform;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = gameObjectTransform.rotation * Quaternion.Euler(90, 180, 0); ;
    }
}
