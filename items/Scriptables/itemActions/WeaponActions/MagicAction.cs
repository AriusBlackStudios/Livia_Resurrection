using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia 
{

    [CreateAssetMenu(menuName = "Item Actions/ Magic Action")]
    public class MagicAction : ItemAction
    {
       
        public override void PerformAction(PlayerManager player)
        {
            base.PerformAction(player);
            if (player.isInteracting) return;
            if (player.inventory.currentSpell != null)
            {
                if (player.stats.currentFocus >= player.inventory.currentSpell.SanityPointCost)
                {
                    player.inventory.currentSpell.AttemptToCastSpell(player.animatorManager, player.stats, player.weaponSlotManager, player.isUsingLeftHand);
                }
                else
                {
                    //  animatorHandler.PlayTargetAnimation("shrug", true);

                }
            }




        }
}
}
