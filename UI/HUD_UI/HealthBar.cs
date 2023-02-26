using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class HealthBar : MonoBehaviour
    {
        public Slider HealthSlider;
        public Slider staminaSlider;
        public Slider focusSlider;

        public void SetMaxHealth(float MaxHealth)
        {
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.value = MaxHealth;

        }

        public void SetCurrentHealth(float currentHealth)
        {
            HealthSlider.value = currentHealth;
        }

        public void SetMaxStamina(float MaxStanima)
        {
            staminaSlider.maxValue = MaxStanima;
            staminaSlider.value = MaxStanima;

        }

        public void SetCurrentStamina(float CurrentStamina)
        {
            staminaSlider.value = CurrentStamina;
        }
        public void SetMaxFocus(float MaxFocus)
        {
            focusSlider.maxValue = MaxFocus;
            focusSlider.value = MaxFocus;

        }

        public void SetCurrentFocus(float CurrentFocus)
        {
            focusSlider.value = CurrentFocus;
        }

    }
}
