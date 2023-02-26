using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Livia
{
    public enum TarotSlot { SlotA, SlotB, SlotC, SlotD, SlotE }

    public class TarotEquiptmentSlotUI : MonoBehaviour
    {
        public Image icon;
        public TarotSlot slot;


        TarotCard tarotCard;
        UIManager uiManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(TarotCard newCard)
        {
            tarotCard = newCard;
            icon.sprite = tarotCard.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            tarotCard = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            if (slot == TarotSlot.SlotA)
            {

                uiManager.currentSlotSelected = SelectedEquipmentSlot.TarotA;
                

            }
            else if (slot == TarotSlot.SlotB)
            {

                uiManager.currentSlotSelected = SelectedEquipmentSlot.TarotB;

            }
            else if (slot == TarotSlot.SlotC)
            {

                uiManager.currentSlotSelected = SelectedEquipmentSlot.TarotC;

            }
            else if (slot == TarotSlot.SlotD)
            {

                uiManager.currentSlotSelected = SelectedEquipmentSlot.TarotD;

            }
            else if (slot == TarotSlot.SlotE)
            {

                uiManager.currentSlotSelected = SelectedEquipmentSlot.TarotE;

            }

            uiManager.tarotStatsWindowUI.UpdateTarotItemStats(tarotCard);


        }
    }
}
