using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    [CreateAssetMenu(menuName = "Item Actions/ Light Attack Action")]
    public class LightAttackAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            
            if (player.stats.currentStamina <= 0) return;
            base.PerformAction(player);
            WeaponItem weapon = player.inventory.rightWeapon;
            if (player.isUsingLeftHand) weapon = player.inventory.leftWeapon;

            //player.animatorHandler.EraseHandIKForWeapon();


            if (player.isSprinting)
            {
                //handle running attack
            }
            if (player.canDoCombo)
            {

                player.animatorManager.anim.SetBool("canDoCombo", false);
                player.inputHandler.comboFlag = true;
                HandleLightAttack(player, weapon, player.isUsingLeftHand);
                player.inputHandler.comboFlag = false;
            }
            else
            {
                if (player.isInteracting) return;
                HandleLightAttack(player,weapon, player.isUsingLeftHand);
                player.animatorManager.anim.SetBool("canDoCombo", true);
            }
            //player.playerEffectsManager.PlayWeaponFX();
        }

        private void HandleLightAttack(PlayerManager player,WeaponItem weapon, bool isLeftHand = false)
        {
            if (player.stats.currentStamina <= 0) return;
            if (player.isInteracting) return;


            string[] weaponAttackString = weapon.ReturnAttackList( player.inputHandler.TwoHand_Flag, false);
            player.effectsManager.PlayerWeaponFX(isLeftHand);
            string nextAttack = player.combatManager.ReturnNextAttackString(weaponAttackString,isLeftHand,player.inputHandler.TwoHand_Flag);
            player.animatorManager.PlayTargetAnimation(nextAttack, true,false,isLeftHand);
            player.combatManager.lastAttack = nextAttack;
            player.weaponSlotManager.attackingWeapon = weapon;

        }

        
    }
}
