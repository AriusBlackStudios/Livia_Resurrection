using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    [CreateAssetMenu(menuName = "Item Actions/ Critical Attack Action")]
    public class CriticalAttackAction : ItemAction
    {

        public override void PerformAction(PlayerManager player)
        {


            if (player.isInteracting) return;
            if (player.stats.currentStamina <= 0) return;

            player.combatManager.AttemptBackstabOrReposte();
            



        }
        
    }
}
