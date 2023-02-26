using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class WeaponPickUp : Interactable
    {
        [Header("Weapon Item")]
        public WeaponItem weapon;



        protected  override void Start() {
            base.Start();

            if (!WorldSaveGameManager.instance.currentCharacterData.itemsInWorld.ContainsKey(itemPickUpID)){
                WorldSaveGameManager.instance.currentCharacterData.itemsInWorld.Add(itemPickUpID,false);
            }
            hasBeenInteractedWith = WorldSaveGameManager.instance.currentCharacterData.itemsInWorld[itemPickUpID];
            if(hasBeenInteractedWith){
                gameObject.SetActive(false);
            }
        }
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);


            //notify character save data that this item has been looted
            if (WorldSaveGameManager.instance.currentCharacterData.itemsInWorld.ContainsKey(itemPickUpID)){
                WorldSaveGameManager.instance.currentCharacterData.itemsInWorld.Remove(itemPickUpID);
            }

            WorldSaveGameManager.instance.currentCharacterData.itemsInWorld.Add(itemPickUpID,true);
            hasBeenInteractedWith = true;
            //places item into the inventory
            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            PlayerAnimatorManager animationHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animationHandler = playerManager.GetComponent<PlayerAnimatorManager>();

            playerManager.InteractableAnimtionTransformReset(playerManager.transform,playerManager.PickUpItem);

            if (weapon.isPrimaryWeapon){
                playerInventory.primaryWeaponInventory.Add(weapon);
                WorldSaveGameManager.instance.currentCharacterData.primaryWeaponItemInventory.Add(weapon.itemID,1);

            }
            else{
                playerInventory.secondaryWeaponInventory.Add(weapon);
                WorldSaveGameManager.instance.currentCharacterData.secondaryWeaponItemInventory.Add(weapon.itemID,1);
            }

            playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            playerManager.itemInteractableGameObject.SetActive(true);
            Destroy(gameObject);

        }
    }
}
