using UnityEngine;

public class MobileDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //workable in webgl also
        gameObject.SetActive(Application.isMobilePlatform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
