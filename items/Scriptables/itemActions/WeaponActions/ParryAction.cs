using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    [CreateAssetMenu(menuName = "Item Actions/ Parry Action")]
    public class ParryAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            base.PerformAction(player);
            if (player.isInteracting) return;
            if (player.inputHandler.TwoHand_Flag)
            {


            }
            else
            {
                player.animatorManager.PlayTargetAnimation(player.inventory.leftWeapon.WeaponArt, true);

            }

        }
    }
}
