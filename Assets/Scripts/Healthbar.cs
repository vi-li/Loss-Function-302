using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void setMaxHealth(float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = max;
    }

    public void setHealth(float health)
    {
        healthSlider.value = health;
        print("health slider is " + healthSlider.value);
    }
}
