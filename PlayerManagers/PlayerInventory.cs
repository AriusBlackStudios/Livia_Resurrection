using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class PlayerInventory : MonoBehaviour
    {
        PlayerManager player;
        [HideInInspector]public QuickSlotsUI quickSlots;

        public SpellItem currentSpell;
        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public ConsumeableItem currentConsumable;
        public WeaponItem unarmedWeapon;


         public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[2]; //gives us two weapons
         public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[2];
         public SpellItem[] spellQuickSlots = new SpellItem[4];
         public ConsumeableItem[] consumeQuickSlots = new ConsumeableItem[15];
        //items
        //potions



        [HideInInspector] public int currentRightHandWeaponIndex = 0;
        [HideInInspector] public int currentLeftHandWeaponIndex = 0;
        [HideInInspector] public int currentSpellIndex = 0;
        [HideInInspector] public int currentConsumableIndex = 0;


        //items with no references
        [Header("Sub-Inventories")]
        public List<WeaponItem> primaryWeaponInventory;
        public List<WeaponItem> secondaryWeaponInventory;
        public List<TarotCard> tarotCards;
        
        [HideInInspector] public List<Collectable> collectableItems;

        //items with no references^^^^^
        private void Awake()
        {
            player = GetComponent<PlayerManager>();
            quickSlots= FindObjectOfType<QuickSlotsUI>();

        }

        private void Start()
        {
            player.weaponSlotManager.LoadBothWeaponsOnSlots();

            quickSlots.UpdateSpellSlotUI(currentSpell);
            quickSlots.UpdateConsumableSlotUI(currentConsumable);


        }

        public void ChangeRightWeapon()
        {
            currentRightHandWeaponIndex++; //increase count first
            if(currentRightHandWeaponIndex > weaponsInRightHandSlots.Length-1)//if count exceeds length of array, equipt unarmed weapon
                currentRightHandWeaponIndex=0;

            if(weaponsInRightHandSlots[currentRightHandWeaponIndex] != null )
            {
                rightWeapon = weaponsInRightHandSlots[currentRightHandWeaponIndex];
                player.weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            }
            


        }


        
        public void ChangeLeftWeapon()
        {
            currentLeftHandWeaponIndex++;
            if (currentLeftHandWeaponIndex > weaponsInLeftHandSlots.Length - 1)
                currentLeftHandWeaponIndex = 0;

            if (weaponsInLeftHandSlots[currentLeftHandWeaponIndex] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftHandWeaponIndex];
                player.weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            }
            


        }

        public void ChangeCurrentSpell()
        {
            currentSpellIndex++;
            if (currentSpellIndex > spellQuickSlots.Length - 1)
            {
                currentSpellIndex = 0;
            }

            currentSpell = spellQuickSlots[currentSpellIndex];
            quickSlots.UpdateSpellSlotUI(currentSpell);

        }

        public void ChangeCurrentConsumable()
        {
            currentConsumableIndex++;
            if (currentConsumableIndex > consumeQuickSlots.Length - 1)
            {
                currentConsumableIndex = 0;
            }

            currentConsumable = consumeQuickSlots[currentConsumableIndex];
            quickSlots.UpdateConsumableSlotUI(currentConsumable);

        }
    }
}
