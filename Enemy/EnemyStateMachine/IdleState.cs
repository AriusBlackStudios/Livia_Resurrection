using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class IdleState : State
    {
        public PursueState pursueState;
        public WanderState wanderState;
        public bool isWandering;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

          //  enemyManager.transform.position = enemyManager.returnToIdleTransform.position;
            enemyManager.currentTarget = null;

            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, enemyManager.detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                PlayerStats characterStats = colliders[i].transform.GetComponent<PlayerStats>();
                if (characterStats != null)
                {
                   // check for team id

                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximimumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                    }
                }
            }
            #endregion

            #region Handle Switching To Next State
            if (isWandering)
            {
                return wanderState;
            }
            else if (enemyManager.currentTarget != null)
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
