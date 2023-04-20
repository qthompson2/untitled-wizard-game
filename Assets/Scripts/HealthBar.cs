using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider tempHealthSlider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI tempHealthText;

    public void SetMaxHealth(int value) {
        healthSlider.maxValue = value;
        healthSlider.minValue = 0;
    }

    public void SetHealth(int value) {
        healthSlider.value = value;
    }
    public void SetMaxTemp(int value) {
        tempHealthSlider.maxValue = value;
        tempHealthSlider.minValue = 0;
    }
    public void SetTemp(int value) {
        tempHealthSlider.value = value;
    }

    void Update() {
        if (tempHealthSlider.value <= 0) {
            SetMaxTemp(0);
        }
        healthText.text = healthSlider.value.ToString();
        tempHealthText.text = tempHealthSlider.value.ToString();
    }
}
