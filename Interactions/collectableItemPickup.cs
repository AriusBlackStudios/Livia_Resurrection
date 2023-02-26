using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Livia
{
    public class collectableItemPickup : Interactable
    {
        public Collectable collectableItem;


        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);

            //pick up the item and add it to the palyer inventory
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            PlayerAnimatorManager animationHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animationHandler = playerManager.GetComponent<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;//stop the player from moving while picking up item
            animationHandler.PlayTargetAnimation("Pick Up Item", true);

            if (collectableItem is TarotCard)
            {
                playerInventory.tarotCards.Add((TarotCard)collectableItem);
            }
            else
            {
                playerInventory.collectableItems.Add(collectableItem);
            }

            playerManager.collectableDisplay.GetComponentInChildren<Text>().text = collectableItem.LiviasComment;
            playerManager.collectableDisplay_image.GetComponent<Image>().sprite = collectableItem.itemImage;

            if (collectableItem.itemImage == null)
                playerManager.collectableDisplay_image.GetComponent<Image>().enabled=false;
            else
                playerManager.collectableDisplay.GetComponentInChildren<Image>().enabled=true;
            playerManager.collectableDisplay.SetActive(true);
            Destroy(gameObject);
        }
    }
}
