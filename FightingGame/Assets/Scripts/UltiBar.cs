using UnityEngine;
using UnityEngine.UI;

public class UltiBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Image ultiIcon;

    public ParticleSystem particle;

    bool particlesPlayed = true;


    public void SetMaxUltiState(float ultiState)
    {
        slider.maxValue = ultiState;
        slider.value = ultiState;
    }

    public void SetUltiState(float ultiState)
    {
        slider.value = ultiState;
        
        fill.color = gradient.Evaluate(ultiState);
    }

    private void Update()
    {
        fill.color = gradient.Evaluate(slider.value);

        if (slider.value >= 1f && !particlesPlayed)
        {
            particlesPlayed = true;
            particle.Play();
        }

        if(slider.value < 1f && particlesPlayed)
        {
            particlesPlayed = false;
        }
    }

}
