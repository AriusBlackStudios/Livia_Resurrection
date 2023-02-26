using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class BossHealth : MonoBehaviour
    {
        public Text bossName;
        public Slider healthSlider;
        private void Awake()
        {
            healthSlider = GetComponentInChildren<Slider>();
        }
        private void Start()
        {
            SetUIHealthBarToInactive();
        }
        public void SetBossName(string name)
        {
            bossName.text = name;
        }

        public void SetUIHealthBarToActive()
        {
            healthSlider.gameObject.SetActive(true);
        }

        public void SetUIHealthBarToInactive()
        {
            healthSlider.gameObject.SetActive(false);
        }

        public void SetBossHealth(float maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;

        }

        public void SetBossCurrentHealth(float health)
        {
            healthSlider.value = health;
        }

    }
}
