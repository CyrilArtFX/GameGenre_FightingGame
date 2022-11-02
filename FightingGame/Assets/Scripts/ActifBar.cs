using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActifBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Image actifIcon;

    public void SetMaxActifState(int actifState)
    {
        slider.maxValue = actifState;
        slider.value = actifState;
    }

    public void SetActifState(int actifState)
    {
        slider.value = actifState;
    }
}
