using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        healthGradient.Evaluate(1f); 
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;

        fill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
}
