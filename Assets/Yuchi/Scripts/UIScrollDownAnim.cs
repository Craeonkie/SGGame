using UnityEngine;
using UnityEngine.UI;

public class UIScrollDownAnim : MonoBehaviour
{
    [SerializeField] private RectTransform ChecklistBackground;
    [SerializeField] private RectTransform downButton;
    [SerializeField] private RectTransform BarBackground;
    Rect desiredShape;
    Vector2 newButtonPos;

    float minChecklistHeight;
    float maxChecklistHeight;
    float quickenAnimSpeed;
    float buttonAnimSpeed;

    bool isChecklistShown;
    bool isChecklistHidden;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isChecklistShown = false;
        isChecklistHidden = true;
        minChecklistHeight = 19.9f;
        maxChecklistHeight = 290.0f;
        quickenAnimSpeed = 500.0f;
        buttonAnimSpeed = 250.0f;

        desiredShape = ChecklistBackground.rect;
        newButtonPos = downButton.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChecklistShown == true)
        {
            if (desiredShape.height < maxChecklistHeight)
            {
                desiredShape.height += Time.deltaTime * quickenAnimSpeed;
                ChecklistBackground.sizeDelta = new Vector2(desiredShape.width, desiredShape.height);

                newButtonPos.y -= Time.deltaTime * buttonAnimSpeed;
                downButton.anchoredPosition = newButtonPos;
            }
        }

        if (isChecklistHidden == true)
        {
            if (desiredShape.height >= minChecklistHeight)
            {
                desiredShape.height -= Time.deltaTime * quickenAnimSpeed;
                ChecklistBackground.sizeDelta = new Vector2(desiredShape.width, desiredShape.height);

                newButtonPos.y += Time.deltaTime * buttonAnimSpeed;
                downButton.anchoredPosition = newButtonPos;
            }
        }
    }

    public void UpdateChecklistStatus()
    {
        if (isChecklistHidden && desiredShape.height <= minChecklistHeight)
        {
            isChecklistHidden = false;
            isChecklistShown = true;
        }
        else if (isChecklistShown && desiredShape.height >= maxChecklistHeight)
        {
            isChecklistHidden = true;
            isChecklistShown = false;
        }
    }
}
