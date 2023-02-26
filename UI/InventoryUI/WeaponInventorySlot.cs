using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class WeaponInventorySlot : MonoBehaviour
    {

        UIManager uiManager;
        WeaponSlotManager weaponSlotManager;

        PlayerManager player;
        public Image icon;
        WeaponItem item;

        private void Awake()
        {

            uiManager = FindObjectOfType<UIManager>();
            weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
            player = FindObjectOfType<PlayerManager>();
        }
        public void AddItem(WeaponItem newItem)
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


            if (uiManager.currentSlotSelected ==SelectedEquipmentSlot.Right01)
            {

                uiManager.playerInventory.primaryWeaponInventory.Add(uiManager.playerInventory.weaponsInRightHandSlots[0]);
                uiManager.playerInventory.weaponsInRightHandSlots[0] = item;
                uiManager.playerInventory.primaryWeaponInventory.Remove(item);
                WorldSaveGameManager.instance.currentCharacterData.primaryWeaponItemInventory.Remove(item.itemID);



            }else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.Right02)
            {

                uiManager.playerInventory.primaryWeaponInventory.Add(uiManager.playerInventory.weaponsInRightHandSlots[1]);
                uiManager.playerInventory.weaponsInRightHandSlots[1] = item;
                uiManager.playerInventory.primaryWeaponInventory.Remove(item);
                WorldSaveGameManager.instance.currentCharacterData.primaryWeaponItemInventory.Remove(item.itemID);

            }
            
            else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.Left01)
            {

                uiManager.playerInventory.primaryWeaponInventory.Add(uiManager.playerInventory.weaponsInLeftHandSlots[0]);
                uiManager.playerInventory.weaponsInLeftHandSlots[0] = item;
                uiManager.playerInventory.primaryWeaponInventory.Remove(item);
                WorldSaveGameManager.instance.currentCharacterData.secondaryWeaponItemInventory.Remove(item.itemID);


            }
            else if (uiManager.currentSlotSelected == SelectedEquipmentSlot.Left02)
            {
                uiManager.playerInventory.primaryWeaponInventory.Add(uiManager.playerInventory.weaponsInLeftHandSlots[1]);
                uiManager.playerInventory.weaponsInLeftHandSlots[1] = item;
                uiManager.playerInventory.primaryWeaponInventory.Remove(item);
                 WorldSaveGameManager.instance.currentCharacterData.secondaryWeaponItemInventory.Remove(item.itemID);

            } 

            else
            {
                return;
            }
            

            //if not unarmed ie not index -1, update current equiptment
            if(uiManager.playerInventory.currentRightHandWeaponIndex >0 && uiManager.playerInventory.currentRightHandWeaponIndex < uiManager.playerInventory.weaponsInRightHandSlots.Length-1){
                uiManager.playerInventory.rightWeapon= uiManager.playerInventory.weaponsInRightHandSlots[uiManager.playerInventory.currentRightHandWeaponIndex];
                Debug.Log("Updating Right Hand");
            }

            if (uiManager.playerInventory.currentLeftHandWeaponIndex > 0 && uiManager.playerInventory.currentLeftHandWeaponIndex < uiManager.playerInventory.weaponsInLeftHandSlots.Length - 1)
                uiManager.playerInventory.leftWeapon = uiManager.playerInventory.weaponsInLeftHandSlots[uiManager.playerInventory.currentLeftHandWeaponIndex];
      
            weaponSlotManager.LoadWeaponOnSlot(uiManager.playerInventory.rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(uiManager.playerInventory.leftWeapon, true);

            uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(uiManager.playerInventory);
            uiManager.ResetAllSelectedSlots();
            player.weaponSlotManager.LoadBothWeaponsOnSlots();

        }

        public void SelectThisButton()
        {
            uiManager.weaponStatsWindowUI.UpdateWeaponStatsUI(item);
        }

    }
}
