using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Livia
{
    public enum HandSlot { Secondary01,Secondary02,Primary01,Primary02};
    public class HandEquiptmentSlotUI : MonoBehaviour
    {
        public Image icon;
        public HandSlot slot;



        WeaponItem weapon;
        UIManager uiManager;

        private void Awake()
        {
            uiManager= FindObjectOfType<UIManager>();
        }
        public void AddItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            icon.sprite = weapon.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            if (slot == HandSlot.Primary01) {
                
                uiManager.currentSlotSelected = SelectedEquipmentSlot.Right01;


            }
            else if (slot == HandSlot.Primary02)
            {
                
                uiManager.currentSlotSelected = SelectedEquipmentSlot.Right02;

            }
            else if (slot == HandSlot.Secondary01)
            {
                
                uiManager.currentSlotSelected = SelectedEquipmentSlot.Left01;

            }
            else if (slot == HandSlot.Secondary02)
            {
                
                uiManager.currentSlotSelected = SelectedEquipmentSlot.Left02;

            }
            uiManager.weaponStatsWindowUI.UpdateWeaponStatsUI(weapon);


        }
    }
}
