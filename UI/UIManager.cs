using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public enum SelectedEquipmentSlot { Nothing, Right01, Right02, Left01, Left02, TarotA, TarotB, TarotC, TarotD, TarotE };
    public class UIManager : MonoBehaviour
    {

        public PlayerInventory playerInventory;
        public PlayerManager player;
        public PlayerFateManager playerFateManager;
        public ItemStatWindowUI weaponStatsWindowUI;
        public ItemStatWindowUI tarotStatsWindowUI;
        public EquiptmentWindowUI equipmentWindowUI;
        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject SelectWindow;
        public GameObject PrimaryWeaponInventory;
        public GameObject SecondaryWeaponInventory;
        public GameObject equipmentWindow;
        public GameObject fortuneWindow;
        public GameObject QuestWindow;
        public GameObject optionsWindow;
        public GameObject WeaponDataWindow;
        public GameObject tarotDataWindow;
        public GameObject settinngsMenu;
        public GameObject levelUpWindow;

        public GameObject firstButton;


        [Header(" Primary Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotParent;
        WeaponInventorySlot[] weaponInventorySlots;

        [Header(" Secondary Weapon Inventory")]
        public GameObject secondaryWeaponInventorySlotPrefab;
        public Transform secondaryWeaponInventorySlotParent;
        WeaponInventorySlot[] secondaryWeaponInventorySlots;


        [Header("Tarot Inventory")]
        public GameObject tarotInventorySlotPrefab;
        public Transform tarotInventorySlotParent;
        TarotInventorySlot[] tarotInventorySlots;


        [Header("Equipment Window Slot Selected")]
        public SelectedEquipmentSlot currentSlotSelected;




        private void Start()
        {
            //initialize array
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
            secondaryWeaponInventorySlots = secondaryWeaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
            tarotInventorySlots = tarotInventorySlotParent.GetComponentsInChildren<TarotInventorySlot>();
            playerFateManager = FindObjectOfType<PlayerFateManager>();
            player = FindObjectOfType<PlayerManager>();

            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(player.inventory);
            equipmentWindowUI.LoadTarotOnEquipmentScreen(playerFateManager);

        }
        public void UpdateUI()
        {
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(player.inventory);
            equipmentWindowUI.LoadTarotOnEquipmentScreen(playerFateManager);

            #region Weapon Inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < player.inventory.primaryWeaponInventory.Count)
                {
                    if (weaponInventorySlots.Length < player.inventory.primaryWeaponInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    
                    weaponInventorySlots[i].AddItem(player.inventory.primaryWeaponInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Secondary Weapon Inventory Slots
            for (int i = 0; i < secondaryWeaponInventorySlots.Length; i++)
            {
                if (i < player.inventory.secondaryWeaponInventory.Count)
                {
                    if (secondaryWeaponInventorySlots.Length < player.inventory.secondaryWeaponInventory.Count)
                    {
                        Instantiate(secondaryWeaponInventorySlotPrefab, secondaryWeaponInventorySlotParent);
                        secondaryWeaponInventorySlots = secondaryWeaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    
                    secondaryWeaponInventorySlots[i].AddItem(player.inventory.secondaryWeaponInventory[i]);
                }
                else
                {
                    secondaryWeaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            //update tarot cards UI
            #region Tarot Inventory Slots
            for (int i = 0; i <tarotInventorySlots.Length; i++)
            {
                if (i < player.inventory.tarotCards.Count)
                {
                    if (tarotInventorySlots.Length < player.inventory.tarotCards.Count)
                    {
                        Instantiate(tarotInventorySlotPrefab, tarotInventorySlotParent);
                       tarotInventorySlots = tarotInventorySlotParent.GetComponentsInChildren<TarotInventorySlot>();
                    }

                    tarotInventorySlots[i].AddItem(player.inventory.tarotCards[i]);
                }
                else
                {
                    tarotInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

        }

        public void OpenSelectWindow()
        {
            SelectWindow.SetActive(true);
            GetComponent<MainMenu>().set_menu_item(firstButton);
        }
        public void CloseSelectWindow()
        {
            SelectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindows()
        {
            ResetAllSelectedSlots();
            PrimaryWeaponInventory.SetActive(false);
            fortuneWindow.SetActive(false);
            equipmentWindow.SetActive(false);
            optionsWindow.SetActive(false);
            QuestWindow.SetActive(false);
            WeaponDataWindow.SetActive(false);
            tarotDataWindow.SetActive(false);
            settinngsMenu.SetActive(false);
            SecondaryWeaponInventory.SetActive(false);

            //spell inventory
            //key items
            //armour
            //ect...
        }

        public void ResetAllSelectedSlots()
        {

            currentSlotSelected = SelectedEquipmentSlot.Nothing;

        }
    }
}
