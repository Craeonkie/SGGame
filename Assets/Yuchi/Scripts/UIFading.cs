using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class UIFading : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadingCanvas;

    float fadingSpeed;

    private bool isFadingIn;
    private bool isFadingOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadingSpeed = 1.2f * Time.deltaTime;
        isFadingIn = false;
        isFadingOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn == true)
        {
            if (fadingCanvas.alpha < 1)
            {
                fadingCanvas.alpha += fadingSpeed;
                if (fadingCanvas.alpha >= 1)
                {
                    isFadingIn = false; //stops the thing from continiously fading in
                }
            }
        }

        if (isFadingOut == true)
        {
            if (fadingCanvas.alpha >= 0)
            {
                fadingCanvas.alpha -= fadingSpeed;
                if (fadingCanvas.alpha <= 0)
                {
                    isFadingOut = false; //stops the thing from continiously fading out
                    transform.parent.gameObject.SetActive(false);
                }

            }
        }
    }

    public void CanvasFadeIn()
    {
        isFadingIn = true;
    }

    public void CanvasFadeOut()
    {
        isFadingOut = true;
    }
}
