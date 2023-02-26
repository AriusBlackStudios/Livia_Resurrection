using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class CombatStanceState : State
    {
        [Header ("Other AI states for This Enemy")]
        public AttackState attackState;
        public PursueState pursueState;

        [Header("Enemy's Basic Attacks")]
        public EnemyAttackAction[] enemyAttacks;

        
        protected float verticalMovment;
        protected float horizontalMovment;
        protected bool randomDestinationSet = false;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

            enemyManager.currentTarget.GetComponent<PlayerManager>().isInCombat = true;

            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            enemyAnimatorManager.anim.SetFloat("Vertical", verticalMovment, 0.2f, Time.deltaTime);
            enemyAnimatorManager.anim.SetFloat("Horizontal", horizontalMovment, 0.2f, Time.deltaTime);
            attackState.hasPerformedAttack = false;
            if (enemyManager.isInteracting)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical",0);
                enemyAnimatorManager.anim.SetFloat("Horizontal", 0);
                return this;
            }


            if (distanceFromTarget > enemyManager.maximumAttackingRange)
            {
                return pursueState;
            }

            if (!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingaction(enemyAnimatorManager);
            }
            //potentially circle around player?
            HandleRotateTowardsTarget(enemyManager);
            GetNewAttack(enemyManager);

            if (enemyManager.currentRecoveryTime <=0 && attackState.currentAttack != null)
            {
                randomDestinationSet = false;
                return attackState;
            }
            
            else
            {
                return this;
            }

        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //rotate manually
            if (enemyManager.isPerformingAction)//if is attacking
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();
                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            //pathfinding
            else
            {
                Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;
                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);

            }


        }

        private void DecideCirclingaction(EnemyAnimatorManager enemyAnimatorManager)
        {
            WalkAroundTarget(enemyAnimatorManager);
        }
        private void WalkAroundTarget(EnemyAnimatorManager enemyAnimationManager)
        {
            verticalMovment = 0.5f;
            horizontalMovment = Random.Range(-1, 1);

            if (horizontalMovment<= 1 && horizontalMovment >=0)
            {
                horizontalMovment = 0.5f;
            }
            if (horizontalMovment >= -1 && horizontalMovment < 0)
            {
                horizontalMovment = -0.5f;
            }
        }

        protected virtual void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);



            int maxScore = 0;
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

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
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];
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


    }
}
