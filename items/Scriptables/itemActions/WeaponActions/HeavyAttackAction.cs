using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    [CreateAssetMenu(menuName = "Item Actions/ Heavy Attack Action")]
    public class HeavyAttackAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            base.PerformAction(player);
            if (player.stats.currentStamina <= 0) return;
            if (player.isInteracting) return;

            WeaponItem weapon = player.inventory.rightWeapon;
            if (player.isUsingLeftHand) weapon = player.inventory.leftWeapon;

            if (player.isSprinting)
            {
                //handle running attack
            }
            if (player.canDoCombo)
            {

                player.animatorManager.anim.SetBool("canDoCombo", false);
                player.inputHandler.comboFlag = true;
                PerformAttack(player, weapon, player.isUsingLeftHand);
                player.inputHandler.comboFlag = false;
            }
            else
            {
                if (player.isInteracting) return;
                PerformAttack(player, weapon, player.isUsingLeftHand);
                player.animatorManager.anim.SetBool("canDoCombo", true);
            }


        }

        private void PerformAttack(PlayerManager player, WeaponItem weapon, bool isLeftHand = false)
        {
            string[] weaponAttackString = weapon.ReturnAttackList(player.inputHandler.TwoHand_Flag, true);
            player.effectsManager.PlayerWeaponFX(isLeftHand);
            string nextAttack = player.combatManager.ReturnNextAttackString(weaponAttackString, isLeftHand, player.inputHandler.TwoHand_Flag);
            player.animatorManager.PlayTargetAnimation(nextAttack, true, false,isLeftHand);
            player.combatManager.lastAttack = nextAttack;
            player.weaponSlotManager.attackingWeapon = weapon;
        }
    }
}
