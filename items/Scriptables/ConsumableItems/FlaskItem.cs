using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    [CreateAssetMenu(menuName ="ConsumeableItems/Consumables/FlaskItem")]
    public class FlaskItem : ConsumeableItem
    {
        [Header("Flask Type")]
        public bool estusFlask;
        public bool ashenFlask;

        [Header("Recovery Amount")]
        public int healthRecoverAmount;
        public int focusPointReconverAmount;


        [Header("RecoveryFX")]
        public GameObject recoveryFX;

        [Header("EmptyFlask")]
        public Sprite emptyImage;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, WeaponSlotManager weaponSlot, PlayerEffectsManager playerEffectsManager,bool twoHandFLag)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlot, playerEffectsManager,twoHandFLag);
            playerEffectsManager.currentParticleFX = recoveryFX;
            playerEffectsManager.healthToBeHealed = healthRecoverAmount;
            playerEffectsManager.sanityToBeHealed = focusPointReconverAmount;
            if (itemModel != null)
            {
                playerEffectsManager.instantiatedFXmodel = Instantiate(itemModel, weaponSlot.rightHandSlot.transform);
            }
            if(!twoHandFLag)
                weaponSlot.rightHandSlot.UnloadWeapon();

            //instantiate flask in hand
            //playrecoveryeffect
            //add heath or FP

        }


    }
}
