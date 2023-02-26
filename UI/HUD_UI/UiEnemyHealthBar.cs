using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Livia
{
    public class UiEnemyHealthBar : MonoBehaviour
    {
        Slider slider;

        float timeUntilBarIsHidden = 0;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void SetHealth(float health)
        {
            slider.value = health;
            timeUntilBarIsHidden = 20;
        }

        public void SetMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void Update()
        {
            if (slider != null)
            {
                transform.LookAt(Camera.main.transform);
                timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;
                if (timeUntilBarIsHidden <= 0)
                {
                    timeUntilBarIsHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                else
                {
                    if (slider.gameObject.activeInHierarchy==false)
                    {
                        slider.gameObject.SetActive(true);
                    }
                }

                if (slider.value <= 0)
                {
                    Destroy(slider.gameObject);
                }
            }
           
        }

    }
}
