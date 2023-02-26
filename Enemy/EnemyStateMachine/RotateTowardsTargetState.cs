using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class RotateTowardsTargetState : State
    {
        public CombatStanceState combatStanceState;

        public bool hasTurningAnimations = true;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

            if (enemyManager.isInteracting) return this;

             enemyManager.currentTarget.GetComponent<PlayerManager>().isInCombat = true;
            if (hasTurningAnimations){
                enemyAnimatorManager.anim.SetFloat("Horizontal", 0);
                enemyAnimatorManager.anim.SetFloat("Vertical", 0);

                Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                float veiwableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);
                /*(if (veiwableAngle >= 100 && veiwableAngle <= 180 && enemyManager.isInteracting ==false)
                {
                    enemyAnimatorManager.PlayTargetAnimationWithRootRotation("turn 180", true);
                    return this;
                }
                if (veiwableAngle <= -100 && veiwableAngle >= -180 && enemyManager.isInteracting == false)
                {
                    enemyAnimatorManager.PlayTargetAnimationWithRootRotation("turn 180", true);
                    return this;
                }*/

                if (veiwableAngle <=-45 && veiwableAngle >= -100 && enemyManager.isInteracting == false)
                {
                    enemyAnimatorManager.PlayTargetAnimationWithRootRotation("turn right", true);
                    return this;
                }
                if (veiwableAngle >= 45 && veiwableAngle <= 100 && enemyManager.isInteracting == false)
                {
                  enemyAnimatorManager.PlayTargetAnimationWithRootRotation("turn left", true);
                  return this;
                }
                return combatStanceState;
            }
            else{
                Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                float veiwableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);
                transform.Rotate(0,veiwableAngle,0,Space.Self);
                return combatStanceState;
            }
        }
    }
}
