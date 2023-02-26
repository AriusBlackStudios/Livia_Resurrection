using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class TarotInventorySlot : MonoBehaviour
    {
        UIManager uiManager;

        WeaponSlotManager weaponSlotManager;
        PlayerFateManager playerFateManager;


        public Image icon;
        TarotCard item;

        private void Awake()
        {

            uiManager = FindObjectOfType<UIManager>();
            weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
            playerFateManager = FindObjectOfType<PlayerFateManager>();
        }
        public void AddItem(TarotCard newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        //called when button is clicked

        public void EquipThisitem()
        {
            //remove current item
            //add current item to inventory
            //equip this new item
            //remove this item from inventory


            if (uiManager.currentSlotSelected == SelectedEquipmentSlot.TarotA)
            {
                if (playerFateManager.card_one != null)
                {
                    playerFateManager.RemoveCardStats(playerFateManager.card_one);
                    uiManager.playerInventory.tarotCards.Add(playerFateManager.card_one);
                }
                uiManager.playerInventory.tarotCards.Remove(item);
                playerFateManager.card_one = item;//replace with new card
                playerFateManager.AddCardStats(playerFateManager.card_one);




            }
            else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.TarotB)
            {
                if (playerFateManager.card_two != null)
                {
                    playerFateManager.RemoveCardStats(playerFateManager.card_two);
                    uiManager.playerInventory.tarotCards.Add(playerFateManager.card_two);
                }
                uiManager.playerInventory.tarotCards.Remove(item);
                playerFateManager.card_two = item;//replace with new card
                playerFateManager.AddCardStats(playerFateManager.card_two);


            }

            else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.TarotC)
            {
                if (playerFateManager.card_Three != null)
                {
                    playerFateManager.RemoveCardStats(playerFateManager.card_Three);
                    uiManager.playerInventory.tarotCards.Add(playerFateManager.card_Three);
                }
                uiManager.playerInventory.tarotCards.Remove(item);
                playerFateManager.card_Three = item;//replace with new card
                playerFateManager.AddCardStats(playerFateManager.card_Three);


            }
            else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.TarotD)
            {
                if (playerFateManager.card_four != null)
                {
                    playerFateManager.RemoveCardStats(playerFateManager.card_four);
                    uiManager.playerInventory.tarotCards.Add(playerFateManager.card_four);
                }
                uiManager.playerInventory.tarotCards.Remove(item);
                playerFateManager.card_four = item;//replace with new card
                playerFateManager.AddCardStats(playerFateManager.card_four);

            }
            else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.TarotE)
            {
                if (playerFateManager.card_Five != null)
                {
                    playerFateManager.RemoveCardStats(playerFateManager.card_Five);
                    uiManager.playerInventory.tarotCards.Add(playerFateManager.card_Five);
                }
                uiManager.playerInventory.tarotCards.Remove(item);
                playerFateManager.card_Five = item;//replace with new card
                playerFateManager.AddCardStats(playerFateManager.card_Five);

            }

            else
            {
                return;
            }


            uiManager.equipmentWindowUI.LoadTarotOnEquipmentScreen(playerFateManager);
            uiManager.ResetAllSelectedSlots();

        }
    }
}
