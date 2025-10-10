using UnityEngine;
using UnityEngine.UI;

public class StartScreenAnim : MonoBehaviour
{
    [SerializeField] RectTransform logoTransform;
    [SerializeField] GameObject GamemodeCanvas;
    [SerializeField] RectTransform gameStartButton;
    private Vector3 logoStartPoint;
    private Vector3 logoEndPoint;
    private Vector3 logoNewPos;
    private Vector3 buttonNewPos;
    private Vector3 buttonStartPoint;
    private Vector3 buttonEndPoint;

    float logoTransitionSpd;
    float buttonTransitionSpd;

    bool isGamemodeCanvasLoaded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logoTransitionSpd = Time.deltaTime * 100.0f;
        buttonTransitionSpd = Time.deltaTime * 180.0f;

        logoStartPoint = new Vector3(0, 20, 0);
        logoEndPoint = new Vector3(0, -220, 0);
        logoTransform.anchoredPosition = logoStartPoint;
        logoNewPos = logoTransform.anchoredPosition;

        buttonStartPoint = new Vector3(0, 650, 0);
        buttonEndPoint = new Vector3(0, 75, 0);
        gameStartButton.anchoredPosition = buttonStartPoint;
        buttonNewPos = gameStartButton.anchoredPosition;

        isGamemodeCanvasLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamemodeCanvasLoaded == false) //if gamemodecanvas is active
        {
            if (logoNewPos.y > logoEndPoint.y)
            {
                Vector3 DisplacementVector = (logoEndPoint - logoStartPoint);
                DisplacementVector.Normalize();
                logoNewPos += DisplacementVector * logoTransitionSpd;
                logoTransform.anchoredPosition = logoNewPos;

            }

            if (buttonNewPos.y > buttonEndPoint.y)
            {
                Vector3 btnDisplacementVector = (buttonEndPoint - buttonStartPoint);
                btnDisplacementVector.Normalize();
                buttonNewPos += btnDisplacementVector * buttonTransitionSpd;
                gameStartButton.anchoredPosition = buttonNewPos;

            }
        }
        //else
        //{
        //    logoTransform.anchoredPosition = logoStartPoint; //return it to start point
        //    gameStartButton.anchoredPosition = buttonStartPoint; //return it to start point
        //}
    }

    public void GamemodeCanvasLoaded()
    {
        isGamemodeCanvasLoaded = true;
        //logoTransform.anchoredPosition = logoStartPoint; //return it to start point
        //gameStartButton.anchoredPosition = buttonStartPoint; //return it to start point
    }

    public void GamemodeCanvasUnloaded()
    {
        isGamemodeCanvasLoaded = false;
        logoTransform.anchoredPosition = logoStartPoint; //return it to start point
        gameStartButton.anchoredPosition = buttonStartPoint; //return it to start point

        logoNewPos = logoTransform.anchoredPosition;
        buttonNewPos = gameStartButton.anchoredPosition;
        Debug.Log("2nd canvas not loaded");
    }
}
