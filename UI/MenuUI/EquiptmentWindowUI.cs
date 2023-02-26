using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    
    public class EquiptmentWindowUI : MonoBehaviour
    {
        public HandEquiptmentSlotUI[] weaponEquipmentSlotUI;
        public TarotEquiptmentSlotUI[] tarotEquipmentSlotUI;

        public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for(int i =0; i < weaponEquipmentSlotUI.Length; i++)
            {
                if (weaponEquipmentSlotUI[i].slot == HandSlot.Primary01)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
                }
                else if (weaponEquipmentSlotUI[i].slot == HandSlot.Primary02)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
                }
                
                else if (weaponEquipmentSlotUI[i].slot == HandSlot.Secondary01)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
                }
                else if (weaponEquipmentSlotUI[i].slot == HandSlot.Secondary02)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
                }
               


            }
        }

        public void LoadTarotOnEquipmentScreen(PlayerFateManager playerFate)
        {
            for (int i = 0; i < tarotEquipmentSlotUI.Length; i++)
            {
                if (tarotEquipmentSlotUI[i].slot == TarotSlot.SlotA && playerFate.card_one != null)
                {
                    tarotEquipmentSlotUI[i].AddItem(playerFate.card_one);
                }
                else if (tarotEquipmentSlotUI[i].slot == TarotSlot.SlotB && playerFate.card_two != null)
                {
                    tarotEquipmentSlotUI[i].AddItem(playerFate.card_two);
                }

                else if (tarotEquipmentSlotUI[i].slot == TarotSlot.SlotC && playerFate.card_Three != null)
                {
                    tarotEquipmentSlotUI[i].AddItem(playerFate.card_Three);
                }
                else if (tarotEquipmentSlotUI[i].slot == TarotSlot.SlotD && playerFate.card_four != null)
                {
                    tarotEquipmentSlotUI[i].AddItem(playerFate.card_four);
                }
                else if (tarotEquipmentSlotUI[i].slot == TarotSlot.SlotE && playerFate.card_Five != null)
                {
                    tarotEquipmentSlotUI[i].AddItem(playerFate.card_Five);
                }



            }
        }





    }
}
