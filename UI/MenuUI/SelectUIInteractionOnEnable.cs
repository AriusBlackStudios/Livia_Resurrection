using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class SelectUIInteractionOnEnable : MonoBehaviour
    {
        public Slider statSlider;

        private void OnEnable()
        {

            statSlider.Select();
            statSlider.OnSelect(null);


        }
    }
}
