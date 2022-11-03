using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ActifBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Image actifIcon;

    public ParticleSystem particle;

    bool particlesPlayed = false;

    public void SetMaxActifState(int actifState)
    {
        slider.maxValue = actifState;
        slider.value = actifState;
    }

    public void SetActifState(int actifState)
    {
        slider.value = actifState;

        fill.color = gradient.Evaluate(actifState);
    }

    private void Update()
    {
        fill.color = gradient.Evaluate(slider.value);

        if (slider.value >= 1f && !particlesPlayed)
        {
            particlesPlayed = true;
            particle.Play();
        }

        if (slider.value < 1f && particlesPlayed)
        {
            particlesPlayed = false;
        }
    }
}
