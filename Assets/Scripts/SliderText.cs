using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] private TMP_Text sliderNum;
    [SerializeField] private Slider slider;
    float sliderNumValue = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSliderNumValue()
    {
        //keep updating the sliderNumValue value to match the slider value
        sliderNumValue = slider.value;
        sliderNum.text = sliderNumValue.ToString();
    }
}
