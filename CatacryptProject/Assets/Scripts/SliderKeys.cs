using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderKeys : MonoBehaviour
{
    public Slider slider;
    public float sliderStep = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slider.value -= sliderStep;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slider.value += sliderStep;
        }

    }
}
