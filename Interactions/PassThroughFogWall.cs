using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class PassThroughFogWall : Interactable
    {
        public EnemyBossManager enemyBossManager;


        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            playerManager.InteractableAnimtionTransformReset(transform, playerManager.LargeDoorInteraction);
            enemyBossManager.StartBossFight();
        }
    }
}
