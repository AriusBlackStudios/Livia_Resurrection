using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float ambushRadius=2;
        public string sleepAnimation;
        public string wakeAnimation;
        public PursueState pursueState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

    
            if (isSleeping && enemyManager.isInteracting==false)
            {
                enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);

            }
            #region Handle Target Detection
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, ambushRadius,enemyManager.detectionLayer);
            for (int i =0; i <colliders.Length; i++)
            {
                PlayerStats characterStats = colliders[i].transform.GetComponent<PlayerStats>();
                if(characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position-enemyManager.transform.position;
                    float viewableAngle=Vector3.Angle(targetDirection,enemyManager.transform.forward);
                    if (viewableAngle> enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximimumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        isSleeping = false;
                        enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }
            #endregion

            #region Handle State Change
            if (enemyManager.currentTarget!= null)
            {
                return pursueState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
