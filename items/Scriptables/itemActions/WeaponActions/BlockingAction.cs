using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{

    [CreateAssetMenu(menuName = "Item Actions/ Blocking Action")]
    public class BlockingAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting) return;
            if (player.isBlocking) return;
            base.PerformAction(player);
            WeaponItem weapon = player.inventory.rightWeapon;
            if (player.isUsingLeftHand) weapon = player.inventory.leftWeapon;

            string blockingAnim = weapon.ReturnBlockingAnim(player.inputHandler.TwoHand_Flag);
            Debug.Log(player.inputHandler.TwoHand_Flag);
            player.animatorManager.PlayTargetAnimation(blockingAnim, false, true, player.isUsingLeftHand); //still can move while blocking
            player.equipmentManager.OpenBlockingCollider();
            player.isBlocking = true;

        }
    }
}
