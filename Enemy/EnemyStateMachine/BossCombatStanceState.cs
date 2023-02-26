using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class BossCombatStanceState : CombatStanceState
    {
        public bool hasPhaseShifted;
        public EnemyAttackAction[] secondPhaseAttacks;

        protected override void GetNewAttack(EnemyManager enemyManager)
        {
            if (hasPhaseShifted)
            {
                Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);



                int maxScore = 0;
                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                         && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            maxScore += enemyAttackAction.attackScore;

                        }
                    }

                }
                int randomValue = Random.Range(0, maxScore);
                int tempScore = 0;
                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];
                    if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                         && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            if (attackState.currentAttack != null) return;
                            tempScore = +enemyAttackAction.attackScore;
                            if (tempScore > randomValue)
                            {
                                attackState.currentAttack = enemyAttackAction;
                                // Debug.Log("Got a new attack: " + attackState.currentAttack.name);
                            }


                        }
                    }

                }
            }
            else
            {
                base.GetNewAttack(enemyManager);
            }
        }
    }
}

