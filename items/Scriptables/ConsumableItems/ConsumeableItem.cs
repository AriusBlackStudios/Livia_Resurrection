using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class ConsumeableItem : Item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Ainimations")]
        public string comsumeAnimation;
        public string comsumeTwoHandAnimation = "TwoHanded Consumable";
        public bool isInteracting;



        public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, WeaponSlotManager weaponSlot,PlayerEffectsManager playerEffectsManager, bool twoHandedFlag)
        {
            if (currentItemAmount>0)
            {
                if(twoHandedFlag)
                playerAnimatorManager.PlayTargetAnimation(comsumeTwoHandAnimation, isInteracting, true);
                else
                    playerAnimatorManager.PlayTargetAnimation(comsumeAnimation, isInteracting, true);
                currentItemAmount -= 1;

                PlayerInventory inventory = playerEffectsManager.GetComponent<PlayerInventory>();
                if (inventory != null)
                {
                    inventory.quickSlots.UpdateConsumableSlotUI(inventory.currentConsumable);
                }

            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Shrug", isInteracting);
            }
        }

        public virtual void ConsumeItem()
        {


        }


    }
}
