using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltiBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Image ultiIcon;

    public void SetMaxUltiState(int ultiState)
    {
        slider.maxValue = ultiState;
        slider.value = ultiState;
    }

    public void SetUltiState(int ultiState)
    {
        slider.value = ultiState;
    }
}
